namespace Lilypad.Helpers; 

public interface IVariable {
    void Get(Function function);
    ExecuteCommand Set(ExecuteCommand execute);
}