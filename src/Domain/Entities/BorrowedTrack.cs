namespace CleanArchitecture.Domain.Entities;

public class BorrowedTrack : AuditableEntity
{
    public int Id { get; set; }

    public int MusicTrackId { get; set; }

    public String UserName { get; set; }= String.Empty;

    public DateTime DateBorrowed { get; set; }

    public DateTime? DateReturned { get; set; }

    public MusicTrack MusicTrack { get; set; } = null!;

}
