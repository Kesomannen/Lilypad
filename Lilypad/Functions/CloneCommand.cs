using Lilypad.Helpers;

namespace Lilypad; 

public class CloneCommand : CloneCommand.ILevel0, CloneCommand.ILevel1, CloneCommand.ILevel2 {
    readonly Function _function;
    
    string _command = "clone";

    public CloneCommand(Function function) {
        _function = function;
    }

    public interface ILevel0 {
        ILevel1 From(EnumReference<Dimension> dimension, Vector3 begin, Vector3 end);
        ILevel1 From(EnumReference<Dimension> dimension, Vector3Range range);
        
        ILevel1 From(Vector3 begin, Vector3 end);
        ILevel1 From(Vector3Range range);
    }

    public interface ILevel1 {
        ILevel2 To(Vector3 destination);
        ILevel2 To(EnumReference<Dimension> dimension, Vector3 destination);
    }

    public interface ILevel2 {
        void Clone();
        void Clone(EnumReference<CloneFilter> filter, EnumReference<CloneMode>? mode = null);
        void Filtered(BlockData filter, EnumReference<CloneMode>? mode = null);
    }

    public ILevel1 From(EnumReference<Dimension> dimension, Vector3 begin, Vector3 end) {
        return Add($"from {dimension} {begin} {end}");
    }

    public ILevel1 From(EnumReference<Dimension> dimension, Vector3Range range) {
        Assert.IsFinite(range, nameof(range));
        return Add($"from {dimension} {range.Min} {range.Max}");
    }

    public ILevel1 From(Vector3 begin, Vector3 end) {
        return Add($"from {begin} {end}");
    }

    public ILevel1 From(Vector3Range range) {
        Assert.IsFinite(range, nameof(range));
        return Add($"from {range.Min} {range.Max}");
    }

    public ILevel2 To(Vector3 destination) {
        return Add($"to {destination}");
    }

    public ILevel2 To(EnumReference<Dimension> dimension, Vector3 destination) {
        return Add($"to {dimension} {destination}");
    }

    public void Clone() {
        Clone(CloneFilter.Replace);
    }

    public void Clone(EnumReference<CloneFilter> filter, EnumReference<CloneMode>? mode = null) {
        mode ??= CloneMode.Normal;
        Add($"{filter} {mode}");
        _function.Add(_command);
    }

    public void Filtered(BlockData filter, EnumReference<CloneMode>? mode = null) {
        mode ??= CloneMode.Normal;
        Add($"filtered {filter} {mode}");
        _function.Add(_command);
    }
    
    CloneCommand Add(string command) {
        _command += $" {command}";
        return this;
    }
}

public enum CloneFilter {
    Replace,
    Masked
}

public enum CloneMode {
    Normal,
    Force,
    Move
}