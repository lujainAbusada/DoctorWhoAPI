using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class DoctorValidator : AbstractValidator<Models.DoctorForCreationDto>
    {
        public DoctorValidator()
        {
            RuleFor(doctor => doctor.LastEpisodeDate).Null().When(doctor => doctor.FirstEpisodeDate == null)
               .WithMessage("If First Episode Date is null, Last Episode Date must be null");
            RuleFor(doctor => int.Parse(doctor.LastEpisodeDate.Substring(doctor.LastEpisodeDate.Length - 4)))
            .GreaterThanOrEqualTo(doctor => int.Parse(doctor.FirstEpisodeDate.Substring(doctor.FirstEpisodeDate.Length - 4)))
            .WithMessage("Last Episode Date should be greater than or equal to First Episode Date ");
        }
    }
}