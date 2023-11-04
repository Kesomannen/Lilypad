﻿namespace Lilypad; 

public class Function : Resource {
    readonly List<CommandGenerator> _generators = new();
    readonly List<string> _commands = new();
    
    bool _isGenerating;
    
    internal Function(string name, Datapack datapack) : base(name, datapack) { }
    
    public Function Add(CommandGenerator generator) {
        if (_isGenerating) {
            generator(this);
        } else {
            _generators.Add(generator);
        }
        return this;
    }
    
    public Function Add(ICommandGenerator generator) {
        return Add(generator.Generate);
    }
    
    public Function Add(Func<string> generator) {
        return Add(f => f.Add(generator()));
    }
    
    public Function Add(Func<IEnumerable<string>> generator) {
        return Add(f => f.Add(generator()));
    }
    
    public Function Add(IEnumerable<string> commands) {
        if (!_isGenerating) {
            return Add(() => commands);
        }
        
        _commands.AddRange(commands);
        return this;
    }
    
    public Function Add(string command) {
        if (!_isGenerating) {
            return Add(() => command);
        }

        _commands.Add(command);
        return this;
    }
    
    public Function SetTick() => AddToMinecraftTag("tick");
    public Function SetLoad() => AddToMinecraftTag("load");

    Function AddToMinecraftTag(string name) {
        var tag = Datapack.FunctionTags.GetOrCreate(name, Namespaces.Minecraft);
        tag.Add(this);
        return this;
    }

    public override string ToString() {
        _isGenerating = true;
        foreach (var generator in _generators) {
            generator(this);
        }
        _isGenerating = false;
        return string.Join('\n', _commands);
    }
}

public delegate void CommandGenerator(Function function);