using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.ProductFavourites.ValueObjects;

public class Title
{
    public string Value { get; }
        
    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Title));
        }

        Value = value;
    }

    public static implicit operator Title(string value) => new(value);

    public static implicit operator string(Title value) => value.Value;
}