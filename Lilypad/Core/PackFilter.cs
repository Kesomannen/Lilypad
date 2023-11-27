using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Filters out files from data packs applied below this one.
/// </summary>
public class PackFilters {
    /// <summary>
    /// Filters out files from data packs applied below this one.
    /// Any file that matches one of these will be treated as if it was not present in the pack at all. 
    /// </summary>
    [JsonProperty("block")]
    public List<Pattern> Patterns = new();

    public struct Pattern {
        /// <summary>
        /// A regular expression for the namespace of files to be filtered out. If unspecified, it applies to every namespace.
        /// </summary>
        public Regex? Namespace;
        
        /// <summary>
        /// A regular expression for the paths of files to be filtered out. If unspecified, it applies to every file.
        /// </summary>
        public Regex? Path;
    }
}