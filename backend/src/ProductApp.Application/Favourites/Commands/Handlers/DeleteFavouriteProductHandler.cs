using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.Exceptions;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Repositories;

namespace ProductApp.Application.Favourites.Commands.Handlers;

public class DeleteFavouriteProductHandler : ICommandHandler<DeleteFavouriteProduct>
{
    private readonly IProductFavouriteRepository _productFavouriteRepository;

    public DeleteFavouriteProductHandler(IProductFavouriteRepository productFavouriteRepository)
    {
        _productFavouriteRepository = productFavouriteRepository;
    }

    public async Task HandleAsync(DeleteFavouriteProduct command)
    {
        var result = await _productFavouriteRepository.GetAsync(command.UserId, new Id(command.ProductId));
        
        if (result is null)
        {
            throw new FavouriteNotFoundException(command.ProductId);
        }
        
        await _productFavouriteRepository.DeleteAsync(result);
    }
}