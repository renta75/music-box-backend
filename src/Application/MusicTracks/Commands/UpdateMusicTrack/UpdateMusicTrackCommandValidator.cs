using FluentValidation;

namespace CleanArchitecture.Application.MusicTracks.Commands.UpdateMusicTrack;

public class UpdateMusicTrackCommandValidator : AbstractValidator<UpdateMusicTrackCommand>
{
    public UpdateMusicTrackCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
