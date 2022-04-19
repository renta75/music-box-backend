using FluentValidation;

namespace CleanArchitecture.Application.MusicTracks.Commands.BorrowMusicTrack;

public class BorrowMusicTrackCommandValidator : AbstractValidator<BorrowMusicTrackCommand>
{
    public BorrowMusicTrackCommandValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(200)
            .NotEmpty();
    }
}
