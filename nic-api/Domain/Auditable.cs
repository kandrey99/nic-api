namespace nic_api.Domain
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }

    public class Auditable : IAuditable
    {
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
