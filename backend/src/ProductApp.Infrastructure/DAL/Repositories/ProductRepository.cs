using Microsoft.EntityFrameworkCore;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.Repositories;
using ProductApp.Core.ProductFavourites.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Product?> GetByExternalIdAsync(ExternalId externalId) =>
        _databaseContext.Product.SingleOrDefaultAsync(x => x.ExternalId == externalId);

    public Task AddAsync(Product product)
    {
        _databaseContext.Product.AddAsync(product);
        return Task.CompletedTask;
    }
}