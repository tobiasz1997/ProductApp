using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.Favourites.Exceptions;
using ProductApp.Core.Favourites.Models;
using ProductApp.Core.Favourites.Repositories;

namespace ProductApp.Application.Favourites.Commands.Handlers;

public class AddFavouriteHandler : ICommandHandler<AddFavourite>
{
    private readonly IClock _clock;
    private readonly IProductFavouriteRepository _productFavouriteRepository;

    public AddFavouriteHandler(IProductFavouriteRepository productFavouriteRepository, IClock clock)
    {
        _clock = clock;
        _productFavouriteRepository = productFavouriteRepository;
    }

    public async Task HandleAsync(AddFavourite command)
    {
        var result = await _productFavouriteRepository.GetAsync(command.UserId, command.ProductId);
        
        if (result is not null)
        {
            throw new FavouriteExistException(command.ProductId); // 409
        }

        var newFavourite = new ProductFavourite(command.UserId, command.ProductId, _clock.Current());
        await _productFavouriteRepository.AddAsync(newFavourite);
    }
}