using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.Favourites.Queries;

public class GetFavouritesList : IQuery<IEnumerable<string>>
{
    public Guid UserId { get; set; }
}