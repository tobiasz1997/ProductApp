using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.ValueObjects;

namespace ProductApp.Core.ProductFavourites.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByExternalIdAsync(ExternalId externalId);
    Task AddAsync(Product product);
}