using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Users.ValueObjects;

public sealed record Token
{
    public string Value { get; }
        
    public Token(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Token));
        }

        Value = value;
    }

    public static implicit operator Token(string value) => new(value);

    public static implicit operator string(Token value) => value.Value;
}