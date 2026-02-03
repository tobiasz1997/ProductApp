using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.Repositories;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;

namespace ProductApp.Infrastructure.DAL.Repositories;

public class UserFavouriteProductsRepository : IUserFavouriteProductsRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserFavouriteProductsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<Product>> GetByUserIdAsync(Id userId)
    {
        var result = await _databaseContext
            .ProductFavourite
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Join(
                _databaseContext.Product.AsNoTracking(),
                favourite => favourite.ProductId,
                product => product.Id,
                (favourite, product) => product
            ).ToListAsync();
        return result;
    }
}