using System.Globalization;

namespace KelimeDefteri.Controllers
{
    public static class CustomMethods
    {
        // extension method to capitilize first letter of form inputs.
        public static string makeFirstCapitilized(this string value)
        {
            return char.ToUpper(value[0], new CultureInfo("tr-TR", false)) + value.Substring(1).ToLower(new CultureInfo("tr-TR", false));
        }
    }
}
