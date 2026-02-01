using ProductApp.Application.Common.Abstraction;
using ProductApp.Core.Favourites.Repositories;

namespace ProductApp.Application.Favourites.Queries.Handlers;

public class GetFavouritesListHandler : IQueryHandler<GetFavouritesList, IEnumerable<string>>
{
    private readonly IProductFavouriteRepository _productFavouriteRepository;

    public GetFavouritesListHandler(IProductFavouriteRepository productFavouriteRepository)
    {
        _productFavouriteRepository = productFavouriteRepository;
    }

    public async Task<IEnumerable<string>> HandleAsync(GetFavouritesList query)
    {
        var result = await _productFavouriteRepository.GetFavouritesAsync(query.UserId);
        return result.Select(x => x.Value);
    }
}