using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<MusicTrack> MusicTracks { get; }

    DbSet<BorrowedTrack> BorrowedTracks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
