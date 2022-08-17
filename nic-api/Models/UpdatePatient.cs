using FluentValidation;
using nic_api.Domain;
using System.ComponentModel.DataAnnotations;

namespace nic_api.Models
{
    public class UpdatePatient
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

        public Sex Sex { get; set; }

        [Range(1, int.MaxValue)]
        public int AreaId { get; set; }
    }

    public class UpdatePatientValidator : AbstractValidator<UpdatePatient>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Необходимо указать имя");
            RuleFor(x => x.MiddleName).NotEmpty().WithMessage("Необходимо указать отчество");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Необходимо указать фамилию");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Необходимо указать адрес");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Необходимо указать дату рождения");
            RuleFor(x => x.Sex).IsInEnum();
            RuleFor(x => x.AreaId).GreaterThan(0);
        }
    }
}
