using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace nic_api.Models;

public class CreateDoctor
{
    [Required]
    [MaxLength(255)]
    public string Fio { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int OfficeId { get; set; }

    [Range(1, int.MaxValue)]
    public int SpecializationId { get; set; }

    [Range(1, int.MaxValue)]
    public int? AreaId { get; set; }
}

public class CreateDoctorValidator : AbstractValidator<CreateDoctor>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.Fio).NotEmpty().WithMessage("Необходимо указать ФИО");
        RuleFor(x => x.OfficeId).GreaterThan(0);
        RuleFor(x => x.SpecializationId).GreaterThan(0);
        RuleFor(x => x.AreaId).GreaterThan(0);
    }
}
