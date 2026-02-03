using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.Favourites.Exceptions;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.Repositories;
using ProductApp.Core.ProductFavourites.ValueObjects;

namespace ProductApp.Application.Favourites.Commands.Handlers;

public class AddFavouriteProductHandler : ICommandHandler<AddFavouriteProduct, Guid>
{
    private readonly IClock _clock;
    private readonly IProductFavouriteRepository _productFavouriteRepository;
    private readonly IProductRepository _productRepository;

    public AddFavouriteProductHandler(IProductFavouriteRepository productFavouriteRepository, IClock clock, IProductRepository productRepository)
    {
        _clock = clock;
        _productRepository = productRepository;
        _productFavouriteRepository = productFavouriteRepository;
    }

    public async Task<Guid> HandleAsync(AddFavouriteProduct command)
    {
        var product = await _productRepository.GetByExternalIdAsync(new ExternalId(command.ExternalId));

        if (product is null)
        {
            var newId = Guid.NewGuid();
            await _productRepository.AddAsync(new Product(newId, command.ExternalId, command.ExternalUrl,
                command.PhotoUrl, command.Title, command.Price, command.Rating, _clock.Current()));

            await _productFavouriteRepository.AddAsync(new ProductFavourite(command.UserId, newId,
                _clock.Current()));
            
            return newId;
        }

        var favourite = await _productFavouriteRepository.GetAsync(command.UserId, product.Id);
        
        if (favourite is not null)
        {
            throw new FavouriteExistException(favourite.ProductId);
        }
            
        await _productFavouriteRepository.AddAsync(new ProductFavourite(command.UserId, product.Id, _clock.Current()));
        return product.Id;
    }
}