using System.ComponentModel.DataAnnotations;

namespace ProductApp.Api.FavouriteProduct.Requests;

public class ProductRequest
{
    [property: Required]
    public string ExternalId { get; set;  }= string.Empty;
    [property: Required]
    public string ExternalUrl { get; set; } = string.Empty;
    [property: Required]
    public string PhotoUrl { get; set; } = string.Empty;
    [property: Required]
    public string Title { get; set; } = string.Empty;
    [property: Required]
    public string Price { get; set; } = string.Empty;
    [property: Required]
    public string Rating { get; set; } = string.Empty;
};