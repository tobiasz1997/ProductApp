using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.DTO;
using ProductApp.Application.Favourites.Mappers;
using ProductApp.Core.Common.Repositories;

namespace ProductApp.Application.Favourites.Queries.Handlers;

public class GetFavouriteProductsListHandler : IQueryHandler<GetFavouriteProductsList, IEnumerable<ProductDto>>
{
    private readonly IUserFavouriteProductsRepository _userFavouriteProductsRepository;

    public GetFavouriteProductsListHandler(IUserFavouriteProductsRepository userFavouriteProductsRepository)
    {
        _userFavouriteProductsRepository = userFavouriteProductsRepository;
    }

    public async Task<IEnumerable<ProductDto>> HandleAsync(GetFavouriteProductsList query)
    {
        var result = await _userFavouriteProductsRepository.GetByUserIdAsync(query.UserId);
        return result.Select(x => x.AsDto());
    }
}