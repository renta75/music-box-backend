using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.MusicTracks.Commands.BorrowMusicTrack;
using CleanArchitecture.Application.MusicTracks.Commands.CreateMusicTrack;
using CleanArchitecture.Application.MusicTracks.Commands.DeleteMusicTrack;
using CleanArchitecture.Application.MusicTracks.Commands.ReturnMusicTrack;
using CleanArchitecture.Application.MusicTracks.Commands.UpdateMusicTrack;
using CleanArchitecture.Application.MusicTracks.Queries.GetBorrowedTracksWithPagination;
using CleanArchitecture.Application.MusicTracks.Queries.GetMusicTracksWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArchitecture.WebUI.Controllers;

//[Authorize(Policy = "api1")]
public class MusicTracksController : ApiControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<MusicTrackDto>>> GetMusicTracksWithPagination([FromQuery] GetMusicTracksWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("/api/musictracks/borrowed")]
    public async Task<ActionResult<PaginatedList<MusicTrackDto>>> GetBorrowedTracksWithPagination([FromQuery] GetBorrowedTracksWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }


    [AllowAnonymous]
    [HttpGet("/picture/{filename}")]
    public async Task<ActionResult<PaginatedList<MusicTrackDto>>> GetCoverPicture(string filename )
    {
        filename = "c:\\music\\covers\\" + filename;

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filename, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var bytes = await System.IO.File.ReadAllBytesAsync(filename);
        return File(bytes, contentType, Path.GetFileName(filename));
    }
    [AllowAnonymous]
    [HttpGet("/media/{filename}")]
    public async Task<ActionResult<PaginatedList<MusicTrackDto>>> GetMediaFile(string filename)
    {
        filename = "c:\\music\\" + filename;

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filename, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var bytes = await System.IO.File.ReadAllBytesAsync(filename);
        return File(bytes, contentType, Path.GetFileName(filename));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMusicTrackCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("/api/musictracks/borrow")]
    public async Task<ActionResult<int>> Borrow(BorrowMusicTrackCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("/api/musictracks/return")]
    public async Task<ActionResult<int>> Return(ReturnMusicTrackCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateMusicTrackCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMusicTrackCommand { Id = id });

        return NoContent();
    }
}
