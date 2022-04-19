using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.MusicTracks.Commands.DeleteMusicTrack;

public class DeleteMusicTrackCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteMusicTrackCommandHandler : IRequestHandler<DeleteMusicTrackCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMusicTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteMusicTrackCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MusicTracks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(MusicTrack), request.Id);
        }

        _context.MusicTracks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
