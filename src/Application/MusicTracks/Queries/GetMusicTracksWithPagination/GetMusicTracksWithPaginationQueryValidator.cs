using FluentValidation;

namespace CleanArchitecture.Application.MusicTracks.Queries.GetMusicTracksWithPagination;

public class GetMusicTracksWithPaginationQueryValidator : AbstractValidator<GetMusicTracksWithPaginationQuery>
{
    public GetMusicTracksWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
