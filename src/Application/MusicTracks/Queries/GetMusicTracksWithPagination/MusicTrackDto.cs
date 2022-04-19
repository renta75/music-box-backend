using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.MusicTracks.Queries.GetMusicTracksWithPagination;

public class MusicTrackDto : IMapFrom<MusicTrack>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Performer { get; set; }

    public string? Filename { get; set; }

    public string? CoverPicture { get; set; }


}
