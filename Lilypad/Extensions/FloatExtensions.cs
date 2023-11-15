namespace Lilypad.Extensions; 

public static class FloatExtensions {
    /// <summary>
    /// Converts seconds to minecraft ticks.
    /// </summary>
    public static int ToTicks(this float seconds) {
        return (int) (seconds * 20);
    }
}