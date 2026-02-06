using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.ProductFavourites.ValueObjects;

public class Price(string? value)
{
    public string? Value { get; } = value;

    public static implicit operator Price(string? value) => new(value);

    public static implicit operator string?(Price? value) => value?.Value;
}