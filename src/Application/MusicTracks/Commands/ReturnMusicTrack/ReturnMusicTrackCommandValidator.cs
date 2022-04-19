using FluentValidation;

namespace CleanArchitecture.Application.MusicTracks.Commands.ReturnMusicTrack;

public class ReturnMusicTrackCommandValidator : AbstractValidator<ReturnMusicTrackCommand>
{
    public ReturnMusicTrackCommandValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(200)
            .NotEmpty();
    }
}
