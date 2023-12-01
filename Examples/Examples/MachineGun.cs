using Lilypad.Helpers;
using Lilypad.ItemModifiers;
using Lilypad.Text;

namespace Lilypad.Examples; 

public class MachineGun : DatapackBuilder {
    protected override string DefaultNamespace => "machine_gun";

    protected override void Build() {
        var give = Datapack.Functions.Create("give");

        ExampleUtil.SetHelpMessage(Datapack, Example.MachineGun,
            "<yellow>Auto reloading crossbow!</yellow>\n \n" +
            $"<u><run {give}>Click here</run></u> or run <i>/function {give}</i> to get it"
        );

        var loadedNbt = new NBT {
            ["Charged"] = true,
            ["ChargedProjectiles"] = new[] {
                new ItemNBT(Item.Arrow)
            }
        };
        
        var item = new ItemNBT(Item.Crossbow) {
            Name = "</i><gold>Machine Gun",
            Lore = new() {
                "<gray></i>Automatic crossbow"
            },
            Enchantments = ItemNBT.VisualOnlyEnchants,
            Unbreakable = true,
            HideFlags = HideFlags.Everything,
            AdditionalTag = loadedNbt + ("machine_gun", true)
        };

        give.Add(f => {
            f.Give("@s", item);
            f.PlaySound(Sounds.Noteblock.Bell, SoundSource.Master, "@s");
        });

        Datapack.Advancements.Create()
            .AddCriteria(Criterion.ShotCrossbow(new ItemConditions {
                Nbt = ("machine_gun", true)
            })).OnComplete(f => {
                f.If(Condition.Data(DataSource.Self, new("SelectedItem", "tag", "machine_gun")), f => { 
                    Reload(f, Slot.Mainhand);
                }).Else(f => {
                    Reload(f, Slot.Offhand);
                });

                void Reload(Function f, Slot slot) {
                    f.ModifyItem(ItemSource.Self, slot, new SetNBT(loadedNbt));
                }
            });
    }
}