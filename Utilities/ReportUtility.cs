using AspNetCore.Reporting;

namespace PosAPI.Utilities
{
    public static class ReportUtility
    {
        public static RenderType GetRenderType(string reportType = "PDF")
        {
            RenderType renderType = reportType.ToUpper() switch
            {
                "EXCEL" => RenderType.Excel,
                "CSV" => RenderType.Excel,
                "WORD" => RenderType.Word,
                "PDF" => RenderType.Pdf
            };
            return renderType;
        }

        public static string GenerateReportName(string fileType)
        {
            var randomSeq = GenerateRandomNumberSequence(6);
            string? doctype = fileType.ToUpper() switch
            {
                "PDF" => "pdf"
            };
            return $"{randomSeq}.{doctype}";
        }

        public static string GenerateRandomNumberSequence(int length)
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < length; i++)
            {
                int randomNumber = random.Next(0, 10); // Generate a random number between 0 and 9
                result += randomNumber.ToString();
            }

            return result;
        }
    }
}
