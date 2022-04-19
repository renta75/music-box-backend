using FluentValidation;

namespace CleanArchitecture.Application.MusicTracks.Queries.GetBorrowedTracksWithPagination;

public class GetBorrowedTracksWithPaginationQueryValidator : AbstractValidator<GetBorrowedTracksWithPaginationQuery>
{
    public GetBorrowedTracksWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
