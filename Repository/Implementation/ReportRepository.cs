using AspNetCore.Reporting;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PosAPI.Data;
using PosAPI.Repository.Interfaces;
using PosAPI.Utilities;
using PosAPI.ViewModels;
using System.Data;
using System.Net.Mime;
using System.Text;

namespace PosAPI.Repository.Implementation
{
    public class ReportRepository : IReportRepository
    {
        private readonly DapperContext _dapper;
        private readonly IWebHostEnvironment _host;
        public ReportRepository(DapperContext dapper, IWebHostEnvironment host)
        {
            _host = host;
            _dapper = dapper;
        }
        public async Task<object> GetInvoiceReport(int id)
        {
            using IDbConnection dbConnection = _dapper.CreateConnection();
            var sp = "exec sp_Transaction_GetByIdReport @id";
            var parameters = new { id = id };
            var invoice = await dbConnection.QueryAsync<InvoiceVM>(sp, parameters);
            var invoiceReport = GenerateReport(invoice.ToList());
            return invoiceReport;
        }
        public object GenerateReport(List<InvoiceVM> model)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string rdlcFilePath = Path.Combine(_host.ContentRootPath, "Reports", "TransactionDetailsReport.rdlc");
            LocalReport report = new LocalReport(rdlcFilePath);
            report.AddDataSource("TransactionDetailsDS", model);

            var parameters = GetReportHeaderData(model[0].CustomerName, model[0].TransactionDate, model[0].PhoneNo);
            var reportResult = report.Execute(ReportUtility.GetRenderType(), 1, parameters);

            var result = new FileContentResult(reportResult.MainStream, MediaTypeNames.Application.Pdf);
            result.FileDownloadName = ReportUtility.GenerateReportName(model[0].InvoiceNo, "PDF");
            return result;
        }

        public Dictionary<string, string> GetReportHeaderData(string customerName, DateTime? TransactionDate, string PhoneNo)
        {
            Dictionary<string, string> parameters = new()
            {
                {"CustomerName",customerName },
                {"InvoiceDate", TransactionDate != null ? TransactionDate.Value.ToString("dd-MMMM-yyyy") : ""},
                {"PhoneNo", PhoneNo }
            };
            return parameters;
        }
    }
}
