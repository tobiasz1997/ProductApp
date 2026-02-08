using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.Favourites.Commands;

public record DeleteFavouriteProduct(Guid UserId, string ProductId) : ICommand;