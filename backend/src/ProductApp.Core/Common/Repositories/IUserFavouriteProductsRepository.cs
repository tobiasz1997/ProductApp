using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;

namespace ProductApp.Core.Common.Repositories;

public interface IUserFavouriteProductsRepository
{ 
    Task<IEnumerable<Product>> GetByUserIdAsync(Id userId);
}