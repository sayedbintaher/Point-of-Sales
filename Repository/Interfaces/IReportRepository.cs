using PosAPI.ViewModels;

namespace PosAPI.Repository.Interfaces
{
    public interface IReportRepository
    {
        Task<object> GetInvoiceReport(int id);
    }
}
