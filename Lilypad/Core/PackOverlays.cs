namespace Lilypad;

/// <summary>
/// Specifies the overlays, which are sub-packs applied over the "normal" contents of a pack.
/// Their directories have their own assets and data directories, and are placed in the pack's root directory. 
/// </summary>
public class PackOverlays {
    /// <summary>
    /// List of overlays. The order is important, as the first in the list will be applied first.
    /// </summary>
    public List<Entry> Entries = new();
    
    public struct Entry {
        /// <summary>
        /// The directory to overlay for the respective versions (allowed characters: a-z, 0-9, _ and -).
        /// </summary>
        public string Directory;
        
        /// <summary>
        /// A range for pack formats when this overlay should be active
        /// </summary>
        public int[] Formats = { PackInformation.NewestFormat };

        public Entry(string directory) {
            Directory = directory;
        }
    }
}