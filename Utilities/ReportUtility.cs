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

        public static string GenerateReportName(string InvoiceNo , string fileType)
        {
            string? doctype = fileType.ToUpper() switch
            {
                "PDF" => "pdf"
            };
            return $"{InvoiceNo}.{doctype}";
        }
    }
}
