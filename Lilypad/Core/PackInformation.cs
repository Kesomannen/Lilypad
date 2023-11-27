using Lilypad.Text;

namespace Lilypad;

public class PackInformation {
    /// <summary>
    /// The description of the datapack.
    /// </summary>
    /// <remarks>
    /// Appears when hovering over the data pack's name in the list given by the <c>/datapack list</c> command, or when viewing the pack in the Create World screen.
    /// </remarks>
    public JsonText Description = JsonText.Empty;
    
    /// <summary>
    /// Pack version. Defaults to <see cref="NewestFormat"/>.
    /// </summary>
    /// <remarks>If this number does not match the current required number, the data pack displays a warning and requires additional confirmation to load the pack.</remarks>
    /// <seealso href="https://minecraft.fandom.com/wiki/Data_pack#Pack_format"/>
    public int PackFormat = NewestFormat;
    
    /// <summary>
    /// Describes a range for pack formats that the pack supports.
    /// </summary>
    public int[]? SupportedFormats;
        
    public const int NewestFormat = 18;
}
