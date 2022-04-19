using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.MusicTracks.Queries.GetMusicTracksWithPagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.MusicTracks.Queries.GetBorrowedTracksWithPagination;

public class GetBorrowedTracksWithPaginationQuery : IRequest<PaginatedList<MusicTrackDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public String UserName { get; set; } = String.Empty;
}

public class GetBorrowedTracksWithPaginationQueryHandler : IRequestHandler<GetBorrowedTracksWithPaginationQuery, PaginatedList<MusicTrackDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBorrowedTracksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MusicTrackDto>> Handle(GetBorrowedTracksWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.MusicTracks
            .Where(x => _context.BorrowedTracks.Any(y => y.MusicTrackId == x.Id && y.DateReturned==null && y.UserName==request.UserName))
            .OrderBy(x => x.Id)
            .ProjectTo<MusicTrackDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
            
    }
}
