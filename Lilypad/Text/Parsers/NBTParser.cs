namespace Lilypad.Text; 

public class NBTParser : ContentParser {
    public override bool TryParse(string tag, string[] arguments, out IEnumerable<TextTag>? tags) {
        tags = null;
        if (tag != "nbt") return false;

        AssertArgumentCount(arguments, 3, 4);

        var source = arguments[1];
        switch (source) {
            case "block":
                if (!Vector3.TryParse(arguments[2], out _)) {
                    throw new ArgumentException($"Expected a valid block position, got {arguments[2]} instead.");
                }
                break;
            case "entity": break;
            case "storage": break;
            default:
                throw new ArgumentException($"Unknown nbt source: {source}");
        }

        var interpret = arguments is [_, _, _, "interpret"];
        tags = GetState(("nbt", arguments[0]), (source, arguments[2]), ("interpret", interpret));
        
        return true;
    }
}