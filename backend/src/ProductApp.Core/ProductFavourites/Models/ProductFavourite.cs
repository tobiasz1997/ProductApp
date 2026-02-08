using ProductApp.Core.Common.ValueObjects;

namespace ProductApp.Core.ProductFavourites.Models;

public class ProductFavourite(Id userId, Id productId, DateTime createdAt)
{
    public Id UserId { get; private set; } = userId;
    public Id ProductId { get; private set; } = productId;
    public DateTime CreatedAt { get; private set; } = createdAt;
}