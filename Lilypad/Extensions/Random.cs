namespace Lilypad.Extensions; 

public static class Random {
    static readonly System.Random _random = new();
    
    public static int Range(int min, int max) {
        return _random.Next(min, max);
    }
    
    public static int NextInt() {
        return _random.Next();
    }
}