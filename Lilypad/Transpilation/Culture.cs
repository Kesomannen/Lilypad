using System.Globalization;

namespace Lilypad;

public static class Culture {
    public static void Initialize() {
        var culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
        culture.NumberFormat.NumberDecimalSeparator = ".";
        CultureInfo.CurrentCulture = culture;
    }
}