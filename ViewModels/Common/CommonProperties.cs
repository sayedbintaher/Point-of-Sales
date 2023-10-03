namespace PosAPI.ViewModels.Common
{
    public class CommonProperties
    {
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }

        public byte? StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
