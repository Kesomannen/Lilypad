namespace Lilypad.Extensions; 

public static class FloatExtensions {
    public static int ToTicks(this float seconds) {
        return (int) (seconds * 20);
    }
}