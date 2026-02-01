using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Users.ValueObjects;

public sealed record Login
{
    public string Value { get; }
        
    public Login(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 2)
        {
            throw new EmptyValueException(nameof(Login));
        }

        Value = value;
    }

    public static implicit operator Login(string value) => new(value);

    public static implicit operator string(Login value) => value.Value;
}