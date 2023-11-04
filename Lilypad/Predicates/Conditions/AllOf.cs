namespace Lilypad.Predicates; 

public class AllOf : Predicate {
    public List<Predicate> Terms;

    public AllOf(IEnumerable<Predicate> terms) {
        Terms = terms.ToList();
    }

    public AllOf() {
        Terms = new List<Predicate>();
    }
}