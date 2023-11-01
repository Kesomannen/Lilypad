namespace Lilypad.Predicates; 

public class AnyOf : Predicate {
    public List<Predicate> Terms = new();
}