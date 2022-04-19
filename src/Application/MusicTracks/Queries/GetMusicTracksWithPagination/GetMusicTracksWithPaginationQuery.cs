using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.MusicTracks.Queries.GetMusicTracksWithPagination;

public class GetMusicTracksWithPaginationQuery : IRequest<PaginatedList<MusicTrackDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetMusicTracksWithPaginationQueryHandler : IRequestHandler<GetMusicTracksWithPaginationQuery, PaginatedList<MusicTrackDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMusicTracksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MusicTrackDto>> Handle(GetMusicTracksWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.MusicTracks
            .OrderBy(x => x.Id)
            .ProjectTo<MusicTrackDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
            
    }
}
