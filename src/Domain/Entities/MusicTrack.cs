namespace CleanArchitecture.Domain.Entities;

public class MusicTrack : AuditableEntity
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Performer { get; set; }

    public string? Filename { get; set; }

    public string? CoverPicture { get; set; }

}
