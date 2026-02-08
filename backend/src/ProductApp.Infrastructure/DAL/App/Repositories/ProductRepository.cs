using Microsoft.EntityFrameworkCore;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.Repositories;
using ProductApp.Core.ProductFavourites.ValueObjects;

namespace ProductApp.Infrastructure.DAL.App.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public ProductRepository(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }

    public Task<Product?> GetByExternalIdAsync(ExternalId externalId) =>
        _appDatabaseContext.Product.SingleOrDefaultAsync(x => x.ExternalId == externalId);

    public Task AddAsync(Product product)
    {
        _appDatabaseContext.Product.AddAsync(product);
        return Task.CompletedTask;
    }
}