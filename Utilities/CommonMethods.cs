namespace PosAPI.Utilities
{
    public static class CommonMethods
    {
        public static DateTime GetBDCurrentTime()
        {
            var Bangladesh_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Bangladesh_Standard_Time);
        }
    }
}
