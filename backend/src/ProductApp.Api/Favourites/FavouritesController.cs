using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Api.Common;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.Commands;
using ProductApp.Application.Favourites.Queries;

namespace ProductApp.Api.Favourites;

[ApiController]
[Authorize]
[Route("favourites")]
public class FavouritesController: BaseApiController
{
    private readonly IQueryHandler<GetFavouritesList, IEnumerable<string>> _getFavouritesList;
    private readonly ICommandHandler<AddFavourite> _addFavourite;
    private readonly ICommandHandler<DeleteFavourite> _deleteFavourite;
    
    public FavouritesController(IQueryHandler<GetFavouritesList, IEnumerable<string>> getFavouritesList, ICommandHandler<AddFavourite> addFavourite, ICommandHandler<DeleteFavourite> deleteFavourite)
    {
        _getFavouritesList = getFavouritesList;
        _addFavourite = addFavourite;
        _deleteFavourite = deleteFavourite;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetFavourites()
    {
        return Ok(await _getFavouritesList.HandleAsync(new GetFavouritesList { UserId = UserId }));
    }
    
    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddFavourite(string productId)
    {
        await _addFavourite.HandleAsync(new AddFavourite(UserId, productId));
        return NoContent();
    }
    
    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteFavourite(string productId)
    {
        await _deleteFavourite.HandleAsync(new DeleteFavourite(UserId, productId));
        return NoContent();
    }
}