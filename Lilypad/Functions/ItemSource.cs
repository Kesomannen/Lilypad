namespace Lilypad; 

public readonly struct ItemSource {
    public readonly ItemSourceType Type;
    public readonly Coordinate? Position;
    public readonly Argument<Selector>? Selector;
    
    public ItemSource(Coordinate position) {
        Type = ItemSourceType.Block;
        Position = position;
    }
    
    public ItemSource(Argument<Selector> selector) {
        Type = ItemSourceType.Entity;
        Selector = selector;
    }

    public override string ToString() {
        return Type switch {
            ItemSourceType.Block => $"block {Position}",
            ItemSourceType.Entity => $"entity {Selector}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static implicit operator ItemSource(Coordinate position) => new(position);
    public static implicit operator ItemSource(Selector selector) => new(selector);
    
    public static ItemSource Block(Coordinate position) => new(position);
    public static ItemSource Entity(Argument<Selector> selector) => new(selector);
}

public enum ItemSourceType {
    Entity,
    Block
}