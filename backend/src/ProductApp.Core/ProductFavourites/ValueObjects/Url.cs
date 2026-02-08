using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.ProductFavourites.ValueObjects;

public class Url
{
    public string Value { get; }
        
    public Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Url));
        }

        Value = value;
    }

    public static implicit operator Url(string value) => new(value);

    public static implicit operator string(Url value) => value.Value;
}