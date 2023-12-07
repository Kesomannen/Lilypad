namespace Lilypad.Helpers;

public static class RaycastHelper {
    public static Raycast DoRaycast(this Function function, Raycast raycast) {
        function.Call(raycast.Function);
        return raycast;
    }
}

[Flags]
public enum SelectorOptions {
    None = 0,
    At = 1,
    As = 2,
    AsAt = At | As
}