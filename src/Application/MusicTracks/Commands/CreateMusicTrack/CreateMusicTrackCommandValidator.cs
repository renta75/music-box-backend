using FluentValidation;

namespace CleanArchitecture.Application.MusicTracks.Commands.CreateMusicTrack;

public class CreateMusicTrackCommandValidator : AbstractValidator<CreateMusicTrackCommand>
{
    public CreateMusicTrackCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
