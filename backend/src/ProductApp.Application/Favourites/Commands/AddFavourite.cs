using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.Favourites.Commands;

public record AddFavourite(Guid UserId, string ProductId) : ICommand;