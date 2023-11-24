using Lilypad.Extensions;

namespace Lilypad.Text.Tests; 

[TestClass]
public class JsonTextParserTests {
    [TestMethod]
    public void Parse_Empty() {
        TestParse("", JsonText.Empty);
    }

    [TestMethod]
    public void Parse_Simple() {
        TestParse("Hello, world!", new JsonText("Hello, world!"));
    }

    [TestMethod]
    public void Parse_ColorFormats() {
        var colors = Enum.GetValues<TextColor>()
            .Select(color => color.ToString().ToSnakeCase());
        
        foreach (var color in colors) {
            TestParse($"<color {color}>Hello, world!</color>", new JsonText(
                ("text", "Hello, world!"),
                ("color", color)
            ));
        }
    }
    
    [TestMethod]
    public void Parse_ColorFormats_Concise() {
        var colors = Enum.GetValues<TextColor>()
            .Select(color => color.ToString().ToSnakeCase());
        
        foreach (var color in colors) {
            TestParse($"<{color}>Hello, world!</color>", new JsonText(
                ("text", "Hello, world!"),
                ("color", color)
            ));
        }
    }

    [TestMethod]
    public void Parse_StyleFormats() {
        var formats = Enum.GetValues<TextStyle>()
            .Select(color => color.ToString().ToSnakeCase());
        
        foreach (var format in formats) {
            TestParse($"<{format}>Hello, world!</{format}>", new JsonText(
                ("text", "Hello, world!"),
                (format, true)
            ));
        }
    }
    
    [TestMethod]
    public void Parse_StyleFormats_Concise() {
        var formats = Enum.GetValues<TextStyle>()
            .Select(color => color.ToString().ToSnakeCase());
        
        foreach (var format in formats) {
            TestParse($"<{format[0]}>Hello, world!</{format[0]}>", new JsonText(
                ("text", "Hello, world!"),
                (format, true)
            ));
        }
    }

    [TestMethod]
    public void Parse_HoverFormat_ShowItem() {
        var nbt = new NBTCompound {
            ["display"] = new NBTCompound {
                ["Name"] = new JsonText(
                    ("text", "God sword"),
                    ("color", "gold")
                )
            }
        }.ToString();
        
        TestParse($"<show_item diamond_sword 1 '{nbt.Escape('\'')}'>Hello, world!", new JsonText(
            ("text", "Hello, world!"),
            ("hoverEvent", new HoverEvent {
                Action = HoverAction.ShowItem,
                Contents = new ItemHoverAction {
                    Item = Item.DiamondSword,
                    Count = 1,
                    Tag = nbt
                }
            })
        ));
    }
    
    [TestMethod]
    public void Parse_HoverFormat_ShowEntity() {
        var text = new JsonText(
            ("text", "Mega Pig"),
            ("color", "red"),
            ("bold", true)
        );

        var uuid = Uuid.New();
        TestParse($"<show_entity pig {uuid} '{text}'>Hello, world!", new JsonText(
            ("text", "Hello, world!"),
            ("hoverEvent", new HoverEvent {
                Action = HoverAction.ShowEntity,
                Contents = new EntityHoverAction {
                    Type = Entity.Pig,
                    Id = uuid,
                    Name = text
                }
            })
        ));
    }

    static void TestParse(string input, JsonText expected) {
        Assert.AreEqual(expected.ToString(), ((JsonText) input).ToString());
    }
}