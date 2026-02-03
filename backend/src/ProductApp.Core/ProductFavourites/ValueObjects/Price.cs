using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.ProductFavourites.ValueObjects;

public class Price
{
    public string Value { get; }
        
    public Price(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Price));
        }

        Value = value;
    }

    public static implicit operator Price(string value) => new(value);

    public static implicit operator string(Price value) => value.Value;
}