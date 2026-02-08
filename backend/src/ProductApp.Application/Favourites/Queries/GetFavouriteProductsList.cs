using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.DTO;

namespace ProductApp.Application.Favourites.Queries;

public class GetFavouriteProductsList : IQuery<IEnumerable<ProductDto>>
{
    public Guid UserId { get; set; }
}