using Lilypad;

namespace Lilypad; 

public class ExecuteCommand {
    readonly List<string> _subCommands = new();
    
    public Function Function { get; }

    public ExecuteCommand(Function function) {
        Function = function;
    }
    
    ExecuteCommand AddSubCommand(string str) {
        _subCommands.Add(str);
        return this;
    }

    public void Generate() {
        Function.Add($"execute {string.Join(" ", _subCommands)}");
    }
    
    public void Run(Function function) {
        AddSubCommand($"run function {function.Location}");
        Generate();
    }
    
    public void Run(Action<Function> build) {
        var function = GetFunction(build);
        function.Generate();
        
        var commands = function.Commands.ToArray();
        if (commands.Length == 1) {
            function.Datapack.Functions.Remove(function);
            AddSubCommand($"run {commands[0]}");
            Generate();
        } else {
            Run(function);
        }
    }

    public void RunStore(Action<Function> build) {
        var function = GetFunction(build);
        function.Generate();
        function.Datapack.Functions.Remove(function);

        var commands = function.Commands.ToArray();
        switch (commands.Length) {
            case 0:
                throw new Exception("Function must contain at least one command");
            case 1:
                AddSubCommand($"run {commands[0]}");
                Generate();
                break;
            default:
                throw new Exception("Store function must contain only one command");
        }
    }
    
    Function GetFunction(Action<Function> build) {
        var name = Names.Get($"{Function.Name}/execute/");
        return Function.Datapack.Functions
            .Create(name, build, Function.Namespace);
    }
    
    public ExecuteCommand As(Argument<Selector> selector) {
        return AddSubCommand($"as {selector}");
    }

    public ExecuteCommand At(Argument<Selector> selector) {
        return AddSubCommand($"at {selector}");
    }

    public ExecuteCommand Align(params EnumReference<Axis>[] axes) {
        return AddSubCommand($"align {string.Join("", axes)}");
    }

    public ExecuteCommand Anchored(EnumReference<Anchor> anchor) {
        return AddSubCommand($"anchored {anchor}");
    }

    public ExecuteCommand Facing(Vector3 pos) {
        return AddSubCommand($"facing {pos}");
    }

    public ExecuteCommand Facing(Argument<Selector> selector, EnumReference<Anchor> anchor) {
        return AddSubCommand($"facing entity {selector} {anchor}");
    }
    
    public ExecuteCommand In(EnumReference<Dimension> dimension) {
        return AddSubCommand($"in {dimension}");
    }
    
    public ExecuteCommand On(EnumReference<Relation> relation) {
        return AddSubCommand($"on {relation}");
    }
    
    public ExecuteCommand Positioned(Vector3 pos) {
        return AddSubCommand($"positioned {pos}");
    }
    
    public ExecuteCommand PositionedAs(Argument<Selector> selector) {
        return AddSubCommand($"positioned as {selector}");
    }
    
    public ExecuteCommand Rotated(Vector2 vector2) {
        return AddSubCommand($"rotated {vector2}");
    }
    
    public ExecuteCommand RotatedAs(Argument<Selector> selector) {
        return AddSubCommand($"rotated as {selector}");
    }
    
    public ExecuteCommand Summon(EnumReference<Entity> entity) {
        return AddSubCommand($"summon {entity}");
    }
    
    public ExecuteCommand If(Condition condition) {
        return AddSubCommand(condition.ToString());
    }
    
    public ExecuteCommand Unless(Condition condition) {
        return AddSubCommand(condition.Not().ToString());
    }
    
    public ExecuteCommand Store(DataSource source, NBTPath path, EnumReference<StoreDataType> type, double scale = 1, bool success = false) {
        var target = success ? "success" : "result";
        return AddSubCommand($"{target} {path} {source} {type} {scale}");
    }

    public ExecuteCommand StoreBossbar() {
        throw new NotImplementedException();
    }
    
    public ExecuteCommand Store(Argument<Selector> selector, Reference<Objective> objective, bool success = false) {
        var target = success ? "success" : "result";
        return AddSubCommand($"store {target} score {selector} {objective}");
    }
}

public enum Relation {
    Attacker,
    Controller,
    Leasher,
    Origin,
    Owner,
    Passengers,
    Target,
    Vehicle
}

public enum StoreDataType {
    Byte,
    Double,
    Float,
    Int,
    Long,
    Short
}