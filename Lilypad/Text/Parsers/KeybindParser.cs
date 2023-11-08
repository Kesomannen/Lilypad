namespace Lilypad.Text; 

public class KeybindParser : ContentParser {
    public override bool TryParse(string tag, string[] arguments, out IEnumerable<TextTag>? tags) {
        if (tag == "keybind") {
            AssertArgumentCount(arguments, 1);
            tags = GetState(("keybind", arguments[0]));
            return true;
        }
        if (tag.StartsWith("key")) {
            AssertArgumentCount(arguments, 0);
            tags = GetState(("keybind", tag));
            return true;
        }
        tags = null;
        return false;
    }
}

public static class Keybind {
    public static readonly string Jump = "key.jump";
    public static readonly string Sneak = "key.sneak";
    public static readonly string Left = "key.left";
    public static readonly string Right = "key.right";
    public static readonly string Back = "key.back";
    public static readonly string Forward = "key.forward";
    public static readonly string Sprint = "key.sprint";
    
    public static readonly string Attack = "key.attack";
    public static readonly string PickItem = "key.pickItem";
    public static readonly string Use = "key.use";
    public static readonly string PlaceBlock = "key.use";
    
    public static readonly string Drop = "key.drop";
    public static string Hotbar(int slot) => $"key.hotbar.{slot + 1}";
    public static readonly string Inventory = "key.inventory";
    public static readonly string SwapHands = "key.swapOffhand";
    
    public static readonly string LoadToolbar = "key.loadToolbarActivator";
    public static readonly string SaveToolbar = "key.saveToolbarActivator";
    
    public static readonly string ListPlayers = "key.playerlist";
    public static readonly string Chat = "key.chat";
    public static readonly string Command = "key.command";
    public static readonly string SocialInteractions = "key.socialInteractions";
    
    public static readonly string Advancements = "key.advancements";
    public static readonly string SpectatorOutlines = "key.spectatorOutlines";
    public static readonly string Screenshot = "key.screenshot";
    public static readonly string SmoothCamera = "key.smoothCamera";
    public static readonly string Fullscreen = "key.fullscreen";
    public static readonly string TogglePerspective = "key.togglePerspective";
}