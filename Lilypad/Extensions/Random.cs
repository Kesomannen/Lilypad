namespace Lilypad.Extensions; 

public static class Random {
    static readonly System.Random _random = new();
    
    public static int NextInt() {
        return _random.Next();
    }
}