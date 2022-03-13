using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class EpisodeValidator : AbstractValidator<Models.EpisodeForCreationDto>
    {
        public EpisodeValidator()
        {
            RuleFor(episode => episode.SeriesNumber.ToString().Length).GreaterThanOrEqualTo(10)
                .WithMessage("Series number should be 10 characters long");
            RuleFor(episode => episode.EpisodeNumber).GreaterThan(0)
                .WithMessage("Episode number should be greater than 0.");

        }
    }
}