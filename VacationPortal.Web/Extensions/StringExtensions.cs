namespace VacationPortal.Web.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            var lower = value.ToLower();

            return lower[0].ToString() + lower.Substring(1);
        }
    }
}
