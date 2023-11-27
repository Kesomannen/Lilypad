namespace Lilypad; 

public class ReferencePredicate : Predicate {
    protected override string Condition => "reference";

    public Reference<PredicateResource> Name;
}