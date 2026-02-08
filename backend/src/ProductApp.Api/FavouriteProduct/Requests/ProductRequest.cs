using System.ComponentModel.DataAnnotations;

namespace ProductApp.Api.FavouriteProduct.Requests;

public class ProductRequest
{
    [property: Required]
    public string ExternalId { get; init;  }= string.Empty;
    [property: Required]
    public string ExternalUrl { get; init; } = string.Empty;
    [property: Required]
    public string PhotoUrl { get; init; } = string.Empty;
    [property: Required]
    public string Title { get; init; } = string.Empty;
    public string? Price { get; init; } = string.Empty;
    public string? Rating { get; init; } = string.Empty;
};