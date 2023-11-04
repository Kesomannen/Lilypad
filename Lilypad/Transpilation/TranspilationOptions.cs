namespace Lilypad; 

public struct TranspilationOptions {
    public bool Overwrite;
    public bool PrettyPrint;
    public string OutputPath;

    public TranspilationOptions(string outputPath) {
        Overwrite = true;
        PrettyPrint = true;
        OutputPath = outputPath;
    }
}