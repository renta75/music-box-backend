using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.MusicTracks.Commands.CreateMusicTrack;

public class CreateMusicTrackCommand : IRequest<int>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public String? Performer { get; set; }

    public String? CoverPicture { get; set; }

    public String? Filename { get; set; }
}

public class CreateMusicTrackCommandHandler : IRequestHandler<CreateMusicTrackCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMusicTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMusicTrackCommand request, CancellationToken cancellationToken)
    {
        var entity = new MusicTrack
        {
            Title = request.Title,
            Performer=request.Performer,
            CoverPicture = request.CoverPicture,
            Filename = request.Filename
        };

        
        _context.MusicTracks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
