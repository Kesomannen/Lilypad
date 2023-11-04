namespace Lilypad.Scoreboards; 

public struct DisplaySlotArgument {
    public EnumReference<DisplaySlot> Type;
    public EnumReference<TextColor>? TeamColor;

    public static implicit operator DisplaySlotArgument(DisplaySlot type) {
        return new DisplaySlotArgument { Type = type };
    }
    
    public static implicit operator DisplaySlotArgument(string type) {
        return new DisplaySlotArgument { Type = type };
    }
    
    public static implicit operator DisplaySlotArgument(TextColor teamColor) {
        return new DisplaySlotArgument {
            Type = DisplaySlot.Sidebar,
            TeamColor = teamColor
        };
    }
    
    public override string ToString() {
        return TeamColor is null ? Type.ToString() : $"{Type}.team.{TeamColor}";
    }
}