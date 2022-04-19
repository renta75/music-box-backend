using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.MusicTracks.Commands.ReturnMusicTrack;

public class ReturnMusicTrackCommand : IRequest<int>
{
    public int Id { get; set; }
    public String UserName { get; set;  } =String.Empty;

}

public class ReturnMusicTrackCommandHandler : IRequestHandler<ReturnMusicTrackCommand, int>
{
    private readonly IApplicationDbContext _context;

    public ReturnMusicTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ReturnMusicTrackCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.BorrowedTracks
            .Where(mt => mt.MusicTrackId == request.Id && mt.DateReturned==null).ToList();

        if (entity == null)
        {
            throw new NotFoundException(nameof(BorrowedTrack), request.Id);
        }

        if (entity.Count>1)
        {
            throw new NotASingleEntityException(nameof(BorrowedTrack), request.Id);

        }

        entity[0].DateReturned=DateTime.Now;


        await _context.SaveChangesAsync(cancellationToken);

        return entity[0].Id;
    }
}
