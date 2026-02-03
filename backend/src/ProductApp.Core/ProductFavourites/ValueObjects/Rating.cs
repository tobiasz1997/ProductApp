using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.ProductFavourites.ValueObjects;

public class Rating
{
    public string Value { get; }
        
    public Rating(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Rating));
        }

        Value = value;
    }

    public static implicit operator Rating(string value) => new(value);

    public static implicit operator string(Rating value) => value.Value;
}