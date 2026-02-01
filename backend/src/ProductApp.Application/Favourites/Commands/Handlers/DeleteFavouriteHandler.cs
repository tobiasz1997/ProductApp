using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.Exceptions;
using ProductApp.Core.Favourites.Repositories;

namespace ProductApp.Application.Favourites.Commands.Handlers;

public class DeleteFavouriteHandler : ICommandHandler<DeleteFavourite>
{
    private readonly IProductFavouriteRepository _productFavouriteRepository;

    public DeleteFavouriteHandler(IProductFavouriteRepository productFavouriteRepository)
    {
        _productFavouriteRepository = productFavouriteRepository;
    }

    public async Task HandleAsync(DeleteFavourite command)
    {
        var result = await _productFavouriteRepository.GetAsync(command.UserId, command.ProductId);
        
        if (result is null)
        {
            throw new FavouriteNotFoundException(command.ProductId);
        }
        
        await _productFavouriteRepository.DeleteAsync(result);
    }
}