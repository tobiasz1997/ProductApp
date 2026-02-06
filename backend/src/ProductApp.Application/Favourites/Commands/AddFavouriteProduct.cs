using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.Favourites.Commands;

public record AddFavouriteProduct(
    Guid UserId,
    string ExternalId,
    string ExternalUrl,
    string PhotoUrl,
    string Title,
    string? Price,
    string? Rating
) : ICommand<Guid>;