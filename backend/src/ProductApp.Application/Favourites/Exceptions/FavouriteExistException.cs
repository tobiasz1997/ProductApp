using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Application.Favourites.Exceptions;

public sealed class FavouriteExistException(string productId) : ConflictException($"Favourite product with id = {productId} is already exist.");