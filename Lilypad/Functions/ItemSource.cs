namespace Lilypad; 

/// <summary>
/// The source of an item for the /item command.
/// </summary>
public struct ItemSource {
    public ItemSourceType Type;
    public Vector3? Position;
    public Argument<Selector>? Selector;
    
    /// <inheritdoc cref="Block"/>
    public ItemSource(Vector3 position) {
        Type = ItemSourceType.Block;
        Position = position;
    }
    
    /// <inheritdoc cref="Entity"/>
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

    public static implicit operator ItemSource(Vector3 position) => new(position);
    public static implicit operator ItemSource(Selector selector) => new(selector);
    
    /// <summary>
    /// Creates an item source from a block at the given position.
    /// </summary>
    public static ItemSource Block(Vector3 position) => new(position);
    
    /// <summary>
    /// Creates an item source from an entity matching the given selector.
    /// </summary>
    public static ItemSource Entity(Argument<Selector> selector) => new(selector);
}

public enum ItemSourceType {
    Entity,
    Block
}