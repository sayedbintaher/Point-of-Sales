namespace PosAPI.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set;}
        public byte StatusId { get; set; }
    }
}
