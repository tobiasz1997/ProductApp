using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Users.ValueObjects;

public sealed record Password
{
    private const int MinLength = 12;
    private const int MaxLength = 64;
    
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Password));
        }

        switch (value.Length)
        {
            case > MaxLength:
                throw new TooLongValueException(nameof(Password), MaxLength);
            case < MinLength:
                throw new TooLongValueException(nameof(Password), MinLength);
        }

        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value.Value;
}