using System.ComponentModel.DataAnnotations;

namespace nic_api.Domain;

public class Doctor : Auditable
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string Fio { get; set; } = null!;

    public int OfficeId { get; set; }
    public Office? Office { get; set; }

    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }

    public int? AreaId { get; set; }
    public Area? Area { get; set; }
}
