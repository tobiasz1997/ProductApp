using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.Repositories;

namespace ProductApp.Infrastructure.DAL.App.Repositories;

public class ProductFavouriteRepository : IProductFavouriteRepository
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public ProductFavouriteRepository(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }


    public async Task<ProductFavourite?> GetAsync(Id userId, Id productId) =>
        await _appDatabaseContext.ProductFavourite.SingleOrDefaultAsync(x =>
            x.UserId == userId && x.ProductId == productId);

    public Task AddAsync(ProductFavourite productFavourite)
    {
        _appDatabaseContext.ProductFavourite.AddAsync(productFavourite);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProductFavourite productFavourite)
    {
        _appDatabaseContext.ProductFavourite.Remove(productFavourite);
        return Task.CompletedTask;
    }
}