using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.MusicTracks.Commands.BorrowMusicTrack;

public class BorrowMusicTrackCommand : IRequest<int>
{
    public int Id { get; set; }
    public String UserName { get; set;  } =String.Empty;

}

public class BorrowMusicTrackCommandHandler : IRequestHandler<BorrowMusicTrackCommand, int>
{
    private readonly IApplicationDbContext _context;

    public BorrowMusicTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(BorrowMusicTrackCommand request, CancellationToken cancellationToken)
    {
        var entity = new BorrowedTrack
        {
            UserName = request.UserName,
            MusicTrackId=request.Id,
            DateBorrowed= DateTime.UtcNow,
            DateReturned=null

        };

        _context.BorrowedTracks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
