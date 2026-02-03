using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.ValueObjects;

namespace ProductApp.Core.ProductFavourites.Models;

public class Product(Id id, ExternalId externalId, Url externalUrl, Url photoUrl, Title title, Price price, Rating rating, DateTime createdAt)
{
    public Id Id { get; private set; } = id;
    public ExternalId ExternalId { get; private set; } = externalId;
    public Url ExternalUrl { get; private set; } = externalUrl;
    public Url PhotoUrl { get; private set; } = photoUrl;
    public Title Title { get; private set; } = title;
    public Price Price { get; private set; } = price;
    public Rating Rating { get; private set; } = rating;
    public DateTime CreatedAt { get; private set; } = createdAt;
}