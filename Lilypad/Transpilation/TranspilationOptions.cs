namespace Lilypad; 

public struct TranspilationOptions {
    /// <summary>
    /// Whether or not to overwrite an existing directory at <see cref="OutputPath"/>.
    /// </summary>
    public bool Overwrite;
    
    /// <summary>
    /// Whether or not to pretty print JSON files (adds indentation and newlines).
    /// </summary>
    public bool PrettyPrint;
    
    /// <summary>
    /// The root directory of the transpiled datapack.
    /// The directory name will be the name of the datapack.
    /// </summary>
    public string OutputPath;
    
    /// <summary>
    /// Creates a new <see cref="TranspilationOptions"/> with the given output path
    /// and default options.
    /// </summary>
    /// <param name="outputPath">See <see cref="OutputPath"/></param>
    public TranspilationOptions(string outputPath) {
        Overwrite = true;
        PrettyPrint = true;
        OutputPath = outputPath;
    }
}