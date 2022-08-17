using nic_api.Domain;
using System.ComponentModel.DataAnnotations;

namespace nic_api.Models
{
    public class IndexPatient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(4000)]
        public string Address { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public IndexArea Area { get; set; } = null!;
    }
}
