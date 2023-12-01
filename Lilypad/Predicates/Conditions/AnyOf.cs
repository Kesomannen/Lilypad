namespace Lilypad; 

public class AnyOf : Predicate {
    public List<Predicate> Terms;
    
    public AnyOf(IEnumerable<Predicate> terms) {
        Terms = terms.ToList();
    }
    
    public AnyOf(params Predicate[] terms) {
        Terms = terms.ToList();
    }
    
    public AnyOf() {
        Terms = new List<Predicate>();
    }
}