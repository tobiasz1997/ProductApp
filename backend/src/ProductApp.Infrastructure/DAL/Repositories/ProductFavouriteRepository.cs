using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.Repositories;

namespace ProductApp.Infrastructure.DAL.Repositories;

public class ProductFavouriteRepository : IProductFavouriteRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductFavouriteRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ProductFavourite?> GetAsync(Id userId, Id productId) =>
        await _databaseContext.ProductFavourite.SingleOrDefaultAsync(x =>
            x.UserId == userId && x.ProductId == productId);

    public Task AddAsync(ProductFavourite productFavourite)
    {
        _databaseContext.ProductFavourite.AddAsync(productFavourite);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProductFavourite productFavourite)
    {
        _databaseContext.ProductFavourite.Remove(productFavourite);
        return Task.CompletedTask;
    }
}