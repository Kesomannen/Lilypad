using Lilypad.Text;

namespace Lilypad; 

public readonly struct DisplaySlotArgument {
    public readonly EnumReference<DisplaySlot> Type;
    public readonly EnumReference<TextColor>? TeamColor;
    
    DisplaySlotArgument(EnumReference<DisplaySlot> type, EnumReference<TextColor>? teamColor) {
        Type = type;
        TeamColor = teamColor;
    }

    public DisplaySlotArgument(EnumReference<DisplaySlot> type) : this(type, null) { }
    
    public static implicit operator DisplaySlotArgument(DisplaySlot type) => new(type);
    public static implicit operator DisplaySlotArgument(string type) => new(type);
    public static implicit operator DisplaySlotArgument(TextColor teamColor) => Team(teamColor);

    public static DisplaySlotArgument Team(EnumReference<TextColor> teamColor) => new(DisplaySlot.Sidebar, teamColor);
    
    public override string ToString() {
        return TeamColor is null ? Type.ToString() : $"{Type}.team.{TeamColor}";
    }
}