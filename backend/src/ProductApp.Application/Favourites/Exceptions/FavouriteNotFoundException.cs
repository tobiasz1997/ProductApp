using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Application.Favourites.Exceptions;

public sealed class FavouriteNotFoundException(string productId) : BadRequestException($"Favourite product with id = {productId} is not exist.");