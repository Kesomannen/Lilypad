namespace Lilypad; 

public class Binomial : NumberProvider {
    public NumberProvider N;
    public NumberProvider P;
    
    public Binomial(NumberProvider n, NumberProvider p) {
        N = n;
        P = p;
    }
}