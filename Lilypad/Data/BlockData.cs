using Lilypad.Extensions;

namespace Lilypad; 

public class BlockData {
    public EnumReference<Block> Block;
    public BlockState? State;
    public NBT? Data;
    
    public object? this[string key] {
        get => State?[key];
        set {
            State ??= new BlockState();
            State[key] = value;
        }
    }
    
    public BlockData(EnumReference<Block> block) {
        Block = block;
    }
    
    public static implicit operator BlockData(EnumReference<Block> block) => new(block);
    public static implicit operator BlockData(Block block) => new(block);
    public static implicit operator BlockData(string block) => new(block);

    public override string ToString() {
        return $"{Block}{State.ToStringOrEmpty()}{Data.ToStringOrEmpty()}";
    }
}