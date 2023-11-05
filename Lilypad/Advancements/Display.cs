using Lilypad.Text;

namespace Lilypad.Advancements; 

public class Display {
    public Icon Icon;
    public EnumReference<Frame>? Frame;
    
    public RichText Title;
    public RichText Description;

    public bool? ShowToast;
    public bool? AnnounceToChat;
    public bool? Hidden;

    public Display(RichText title, RichText description, EnumReference<Item> icon) 
        : this(title, description, new Icon { Item = icon }) { }
    
    public Display(RichText title, RichText description, Icon icon) {
        Title = title;
        Description = description;
        Icon = icon;
    }
}

public struct Icon {
    public EnumReference<Item> Item;
    public string? Nbt;
}