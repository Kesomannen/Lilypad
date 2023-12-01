using Lilypad.Text;

namespace Lilypad; 

public class Display {
    public Icon Icon;
    public EnumReference<Frame>? Frame;
    
    public JsonText Title;
    public JsonText Description;

    public bool? ShowToast;
    public bool? AnnounceToChat;
    public bool? Hidden;

    public Display(JsonText title, JsonText description, EnumReference<Item> icon) 
        : this(title, description, new Icon { Item = icon }) { }
    
    public Display(JsonText title, JsonText description, Icon icon) {
        Title = title;
        Description = description;
        Icon = icon;
    }
}

public struct Icon {
    public EnumReference<Item> Item;
    public NBT? Nbt;
}