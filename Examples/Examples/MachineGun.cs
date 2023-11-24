using Lilypad.Extensions;
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

        var currentTick = CurrentTick.Get(Datapack);

        const int maxAmmo = 10;
        const float reloadTime = 1.5f;
        
        var nbt = new NBTCompound {
            ["machine_gun"] = true,
            ["reload_start"] = long.MinValue,
            ["display"] = new NBTCompound {
                ["Name"] = (JsonText) $"</i><gold>Machine Gun <gray>({maxAmmo})",
                ["Lore"] = new [] {
                    (JsonText) $"<gray></i>Insane fire rate, but needs to reload after {maxAmmo} shots",
                }
            },
            ["Enchantments"] = new [] {
                new NBTCompound()
            },
            ["Unbreakable"] = true,
            ["HideFlags"] = HideFlags.Everything
        };

        var loadedNbt = new NBTCompound {
            ["ammo"] = maxAmmo,
            ["Charged"] = true,
            ["ChargedProjectiles"] = new[] {
                new NBTCompound {
                    ["id"] = Item.Arrow,
                    ["Count"] = (sbyte)1
                }
            }
        };

        give.Add(f => {
            f.Give("@s", Item.Crossbow, nbt + loadedNbt);
            f.PlaySound(Sounds.Noteblock.Pling, SoundSource.Master, "@s");
        });

        Datapack.Advancements.Create("shoot_crossbow")
            .AddCriteria(Criterion.ShotCrossbow(new ItemConditions { Nbt = ("machine_gun", true) }))
            .OnComplete(f => {
                var ammo = GetTagOfSelectedItem(f, "ammo");

                f.If(Condition.Variable(f, ammo, (1, int.MaxValue)), f => {
                    f.Operation(ammo, "subtract", 1);
                    f.ModifyItem(ItemSource.Executor, Slot.Mainhand,
                        CopyNbt.FromVariable(f, ammo, "ammo"), 
                        new SetName($"</i><gold>Machine Gun <gray>(<{ammo}>)")
                    );

                    f.If(Condition.Variable(f, ammo, 0), f => {
                        f.ModifyItem(ItemSource.Executor, Slot.Mainhand,
                            CopyNbt.FromVariable(f, currentTick, "reload_start", dataType: "long"),
                            new SetName("</i><gold>Machine Gun <gray>(<red>RELOADING<gray>)")
                        );
                    });
                }).Else(f => {
                    f.Tellraw("<red>No ammo!");
                });
            });

        Datapack.Advancements.Create("used_crossbow")
            .AddCriteria(Criterion.UsingItem(new ItemConditions {
                Nbt = new NBTCompound {
                    ["machine_gun"] = true,
                    ["ammo"] = 0
                }
            })).OnComplete(f => {
                var reloadStart = GetTagOfSelectedItem(f, "reload_start", "long");
                var delta = f.Evaluate($"{currentTick} - {reloadStart}", currentTick, reloadStart);

                f.If(Condition.Variable(f, delta, (reloadTime.ToTicks(), int.MaxValue)), f => {
                    f.Tellraw("<green>Reloaded!");
                    f.ModifyItem(ItemSource.Executor, Slot.Mainhand,
                        new SetNbt(loadedNbt),
                        new SetName($"</i><gold>Machine Gun <gray>({maxAmmo})")
                    );
                }).Else(f => {
                    f.ModifyItem(ItemSource.Executor, Slot.Mainhand, new SetNbt(("Charged", false)));
                });
            });
    }

    static DataVariable GetTagOfSelectedItem(Function function, string tag, EnumReference<StoreDataType>? dataType = null) {
        var path = new NBTPath("SelectedItem", "tag", tag);
        var variable = new DataVariable(DataSource.Executor, dataType ?? StoreDataType.Int, path);
        return function.CopyTempStorage(variable, path, dataType);
    }
}