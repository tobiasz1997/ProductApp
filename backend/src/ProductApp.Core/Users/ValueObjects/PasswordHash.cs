using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Users.ValueObjects;

public sealed record PasswordHash
{
    public string Value { get; }
        
    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InternalException();
        }

        Value = value;
    }

    public static implicit operator PasswordHash(string value) => new(value);

    public static implicit operator string(PasswordHash value) => value.Value;
}