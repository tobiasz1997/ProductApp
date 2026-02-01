using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Favourites.ValueObjects;

public sealed record ProductId
{
    public string Value { get; }
        
    public ProductId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(ProductId));
        }

        Value = value;
    }

    public static implicit operator ProductId(string value) => new(value);

    public static implicit operator string(ProductId value) => value.Value;
}