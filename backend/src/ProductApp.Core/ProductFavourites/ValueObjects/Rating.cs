namespace ProductApp.Core.ProductFavourites.ValueObjects;

public class Rating(string? value)
{
    public string? Value { get; } = value;

    public static implicit operator Rating(string value) => new(value);

    public static implicit operator string?(Rating? value) => value?.Value;
}