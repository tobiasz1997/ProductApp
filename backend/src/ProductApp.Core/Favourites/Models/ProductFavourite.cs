using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Favourites.ValueObjects;

namespace ProductApp.Core.Favourites.Models;

public class ProductFavourite(Id userId, ProductId productId, DateTime createdAt)
{
    public Id UserId { get; private set; } = userId;
    public ProductId ProductId { get; private set; } = productId;
    public DateTime CreatedAt { get; private set; } = createdAt;
}