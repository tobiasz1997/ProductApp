using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Users.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 2)
        {
            throw new EmptyValueException(nameof(Password));
        }

        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value.Value;
}