using ConfigurationsTask.Entities.Dtos.Brands;
using FluentValidation;

namespace ConfigurationsTask.Validators.Brands;

public class CreateBrandDtoValidator:AbstractValidator<CreateBrandDto>
{
    public CreateBrandDtoValidator()
    {
        RuleFor(b => b.Name).MaximumLength(30).MinimumLength(3).WithMessage("Name must be between 30 and 3 characters long.")
            .Must(StartWithA).WithMessage("A ile abslayan soz daxil edin");
    }

    public bool StartWithA(string name)
    {
        return name.StartsWith('A');
    }
}