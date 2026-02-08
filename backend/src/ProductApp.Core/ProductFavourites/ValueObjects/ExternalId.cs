using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.ProductFavourites.ValueObjects;

public sealed record ExternalId
{
    public string Value { get; }
        
    public ExternalId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(ExternalId));
        }

        Value = value;
    }

    public static implicit operator ExternalId(string value) => new(value);

    public static implicit operator string(ExternalId value) => value.Value;
}