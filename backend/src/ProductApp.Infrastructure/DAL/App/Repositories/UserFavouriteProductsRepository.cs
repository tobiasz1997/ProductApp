using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.Repositories;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;

namespace ProductApp.Infrastructure.DAL.App.Repositories;

public class UserFavouriteProductsRepository : IUserFavouriteProductsRepository
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public UserFavouriteProductsRepository(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }

    public async Task<IEnumerable<Product>> GetByUserIdAsync(Id userId)
    {
        var result = await _appDatabaseContext
            .ProductFavourite
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Join(
                _appDatabaseContext.Product.AsNoTracking(),
                favourite => favourite.ProductId,
                product => product.Id,
                (favourite, product) => product
            ).ToListAsync();
        return result;
    }
}