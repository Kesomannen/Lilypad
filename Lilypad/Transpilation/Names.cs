namespace Lilypad; 

internal static class Names {
    static int _id;
    
    public static string Get() => $"gen_{_id++}";
}