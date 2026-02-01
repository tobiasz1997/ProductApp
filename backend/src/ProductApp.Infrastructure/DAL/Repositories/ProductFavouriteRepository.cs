using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Favourites.Models;
using ProductApp.Core.Favourites.Repositories;
using ProductApp.Core.Favourites.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Repositories;

public class ProductFavouriteRepository : IProductFavouriteRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductFavouriteRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ProductFavourite?> GetAsync(Id userId, ProductId productId) =>
        await _databaseContext.ProductFavourites.SingleOrDefaultAsync(x =>
            x.UserId == userId && x.ProductId == productId);

    public Task AddAsync(ProductFavourite productFavourite)
    {
        _databaseContext.ProductFavourites.AddAsync(productFavourite);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProductFavourite productFavourite)
    {
        _databaseContext.ProductFavourites.Remove(productFavourite);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<ProductId>> GetFavouritesAsync(Id userId) => await _databaseContext.ProductFavourites
        .AsNoTracking()
        .Where(x => x.UserId == userId)
        .Select(x => x.ProductId)
        .ToListAsync();
}