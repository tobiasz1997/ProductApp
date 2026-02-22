using System.Text.RegularExpressions;
using ProductApp.Core.Common.Exceptions;

namespace ProductApp.Core.Users.ValueObjects;

public sealed record Login
{
    private const int MinLength = 5;
    private const int MaxLength = 50;
    private static readonly Regex PolishCharactersRegex = new("[ĄąĘęÓóŚsŁłŻżŹźĆćŃń]", RegexOptions.Compiled);
    private static readonly Regex SpecialCharactersRegex = new("^[a-zA-Z0-9ĄąĘęÓóŚśŁłŻżŹźĆćŃń]+$", RegexOptions.Compiled);
    
    public string Value { get; }
        
    public Login(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Login));
        }

        switch (value.Length)
        {
            case > MaxLength:
                throw new TooLongValueException(nameof(Login), MaxLength);
            case < MinLength:
                throw new TooShortValueException(nameof(Login), MinLength);
        }

        if (!value.Any(char.IsUpper))
        {
            throw new UpperLetterValueException(nameof(Login));
        }
        
        if (!value.Any(char.IsLower))
        {
            throw new LowerLetterValueException(nameof(Login));
        }
        
        if (!value.Any(char.IsDigit))
        {
            throw new NoDigitValueException(nameof(Login));
        }
        
        if (PolishCharactersRegex.IsMatch(value))
        {
            throw new InvalidCharValueException(value);
        }
        
        if (!SpecialCharactersRegex.IsMatch(value))
        {
            throw new InvalidCharValueException(value);
        }

        Value = value;
    }

    public static implicit operator Login(string value) => new(value);

    public static implicit operator string(Login value) => value.Value;
}