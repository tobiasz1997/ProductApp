using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Api.Common;
using ProductApp.Api.FavouriteProduct.Requests;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.Commands;
using ProductApp.Application.Favourites.DTO;
using ProductApp.Application.Favourites.Queries;

namespace ProductApp.Api.Favourites;

[ApiController]
[Authorize]
[Route("favourites")]
public class FavouriteProductController: BaseApiController
{
    private readonly IQueryHandler<GetFavouriteProductsList, IEnumerable<ProductDto>> _getFavouriteProductsList;
    private readonly ICommandHandler<AddFavouriteProduct, Guid> _addFavouriteProduct;
    private readonly ICommandHandler<DeleteFavouriteProduct> _deleteFavouriteProduct;


    public FavouriteProductController(IQueryHandler<GetFavouriteProductsList, IEnumerable<ProductDto>> getFavouriteProductsList, ICommandHandler<AddFavouriteProduct, Guid> addFavouriteProduct, ICommandHandler<DeleteFavouriteProduct> deleteFavouriteProduct)
    {
        _getFavouriteProductsList = getFavouriteProductsList;
        _addFavouriteProduct = addFavouriteProduct;
        _deleteFavouriteProduct = deleteFavouriteProduct;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetFavouriteProducts()
    {
        return Ok(await _getFavouriteProductsList.HandleAsync(new GetFavouriteProductsList() { UserId = UserId }));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> AddFavouriteProduct([FromBody] ProductRequest command)
    {
        var id = await _addFavouriteProduct.HandleAsync(new AddFavouriteProduct(UserId, command.ExternalId,
            command.ExternalUrl, command.PhotoUrl, command.Title, command.Price, command.Rating));
        return StatusCode(StatusCodes.Status201Created, id);
    }
    
    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteFavouriteProduct(string productId)
    {
        await _deleteFavouriteProduct.HandleAsync(new DeleteFavouriteProduct(UserId, productId));
        return NoContent();
    }
}