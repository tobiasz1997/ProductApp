using ProductApp.Application.Favourites.DTO;
using ProductApp.Core.ProductFavourites.Models;

namespace ProductApp.Application.Favourites.Mappers;

public static class ProductMapper
{
    public static ProductDto AsDto(this Product entity)
        => new()
        {
            Id = entity.Id,
            ExternalId = entity.ExternalId,
            ExternalUrl = entity.ExternalUrl,
            PhotoUrl = entity.PhotoUrl,
            Title = entity.Title,
            Price = entity.Price,
            Rating = entity.Rating
        };
}