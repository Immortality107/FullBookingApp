
    using System.Globalization;
public static class CountryHelper
{
    public static List<string> GetAllCountries(string culture = "en")
    {
        // استخدم CultureInfo للغة المطلوبة
        var cultureInfo = new CultureInfo(culture);

        return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(c => new RegionInfo(c.Name))
            .GroupBy(r => r.Name) // علشان ميكررش الدول
            .Select(g => new RegionInfo(g.Key))
            .Select(r => cultureInfo.TwoLetterISOLanguageName == "ar" ? r.NativeName : r.EnglishName)
            .Distinct()
            .OrderBy(name => name)
            .ToList();
    }
}


