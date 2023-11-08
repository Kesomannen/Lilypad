namespace Lilypad; 

public class Inverted : Predicate {
    public Predicate Term;

    public Inverted(Predicate term) {
        Term = term;
    }
}