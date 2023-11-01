namespace Lilypad.Text; 

public struct HexColorFormat : ITextFormat {
    string _hexString;
    
    public string HexString {
        get => _hexString;
        set {
            if (value.Length != 6) {
                throw new ArgumentException("Hex color must be 6 characters long.");
            }
            _hexString = value;
        }
    }
    
    public string Name => "color";
    public object Value => $"#{HexString}";
}