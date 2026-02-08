using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;

namespace ProductApp.Core.ProductFavourites.Repositories;

public interface IProductFavouriteRepository
{
    Task<ProductFavourite?> GetAsync(Id userId, Id productId);
    Task AddAsync(ProductFavourite productFavourite);
    Task DeleteAsync(ProductFavourite productFavourite);
}