using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.MusicTracks.Commands.UpdateMusicTrack;

public class UpdateMusicTrackCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public String? Performer { get; set; }

    public String? CoverPicture { get; set; }

    public String? Filename { get; set; }
}

public class UpdateMusicTrackCommandHandler : IRequestHandler<UpdateMusicTrackCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMusicTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateMusicTrackCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MusicTracks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(MusicTrack), request.Id);
        }

        entity.Title = request.Title;
        entity.Performer = request.Performer;
        entity.CoverPicture = request.CoverPicture;
        entity.Filename = request.Filename;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
