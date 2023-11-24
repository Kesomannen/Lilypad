namespace Lilypad.Helpers; 

public interface IVariable {
    void Get(Function function);
    ExecuteCommand Set(ExecuteCommand execute);
    
    protected static string GetName(params object[] segments) {
        return string.Join(Separator, segments).Replace(" ", Space.ToString());
    }
    
    public const char Separator = '&';
    public const char Space = '$';
}