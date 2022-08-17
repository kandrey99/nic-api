using System.ComponentModel.DataAnnotations;

namespace nic_api.Domain;

public class Patient
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string MiddleName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(4000)]
    public string Address { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    public Sex Sex { get; set; }

    public int AreaId { get; set; }
    public Area? Area { get; set; }
}

public enum Sex
{
    Male,
    Female
}