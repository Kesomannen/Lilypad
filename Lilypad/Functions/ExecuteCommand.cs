namespace Lilypad; 

/// <summary>
/// Builder for execute commands.
/// </summary>
/// <remarks>
/// Is not automatically added ("generated") to its owner function,
/// unless <see cref="Run(Lilypad.Reference{Lilypad.Function})"/> or <see cref="ExecuteCommand.RunStore"/> is called.
/// <br/><br/>When you don't call one of those, for example when storing the result of an if condition,
/// simply call <see cref="ExecuteCommand.Generate"/> to add the command automatically.
/// </remarks>
public class ExecuteCommand {
    readonly List<string> _subCommands = new();
    readonly Function _function;

    /// <summary>
    /// Creates a new execute command builder in the function.
    /// </summary>
    public ExecuteCommand(Function function) {
        _function = function;
    }
    
    ExecuteCommand AddSubCommand(string str) {
        _subCommands.Add(str);
        return this;
    }

    /// <summary>
    /// Generates the string representation of the command and adds it the owner function.
    /// </summary>
    /// <remarks>Don't use this object after generation.</remarks>
    public void Generate() {
        _function.Add($"execute {string.Join(" ", _subCommands)}");
    }
    
    /// <summary>
    /// Finalizes the execute command by calling another function.
    /// </summary>
    /// <remarks>Generates the command. Don't use this object after generation.</remarks>
    public void Run(Reference<Function> function) {
        AddSubCommand($"run function {function}");
        Generate();
    }
    
    /// <summary>
    /// Finalizes the execute command by calling a number of commands.
    /// </summary>
    /// <param name="build">
    /// Action that generates the command(s) to run. Called immediately on a dummy function.
    /// <br/><br/>If the dummy function contains only one command after generation, it is inlined and the function deleted.
    /// Otherwise the dummy is kept and a call is generated.
    /// </param>
    /// <remarks>
    /// Generates the command. Don't use this object after generation.
    /// </remarks>
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

    /// <summary>
    /// Finalizes the execute command by calling a command that returns some value to store.
    /// </summary>
    /// <param name="build">
    /// Action that generates the command to run. Called immediately on a temporary function.
    /// </param>
    /// <remarks>
    /// Generates the command. Don't use this object after generation.
    /// </remarks>
    /// <exception cref="Exception">Thrown if the dummy function does not contain exactly one command after generation.</exception>
    /// <seealso cref="Store(Lilypad.DataSource,Lilypad.NBTPath,Lilypad.EnumReference{Lilypad.StoreDataType},double,bool)"/>
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
        var name = Names.Get($"{_function.Name}/execute/");
        return _function.Datapack.Functions
            .Create(name, build, _function.Namespace);
    }
    
    public ExecuteCommand As(Argument<Selector> selector) {
        return AddSubCommand($"as {selector}");
    }

    public ExecuteCommand At(Argument<Selector> selector) {
        return AddSubCommand($"at {selector}");
    }
    
    public ExecuteCommand AsAt(Argument<Selector> selector) {
        return As(selector).At(Selector.Self);
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
        return AddSubCommand($"store {target} {source} {path} {type} {scale}");
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