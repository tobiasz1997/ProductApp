using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Favourites.Models;
using ProductApp.Core.Favourites.ValueObjects;

namespace ProductApp.Core.Favourites.Repositories;

public interface IProductFavouriteRepository
{
    Task<ProductFavourite?> GetAsync(Id userId, ProductId productId);
    Task AddAsync(ProductFavourite productFavourite);
    Task DeleteAsync(ProductFavourite productFavourite);
    Task<IEnumerable<ProductId>> GetFavouritesAsync(Id userId);
}