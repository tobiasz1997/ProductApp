using ProductApp.Core.Common.Exceptions;
using FormatException = ProductApp.Core.Common.Exceptions.FormatException;

namespace ProductApp.Core.Common.ValueObjects;

public sealed record Id
{
    public Guid Value { get; }

    public Id(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyValueException(nameof(Id));
        }

        Value = value;
    }
    
    public Id(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Id));
        }

        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new FormatException(nameof(Id));
        }

        Value = guidValue;
    }

    public static implicit operator Guid(Id id) => id.Value;

    public static implicit operator Id(Guid id) => new(id);
    
    public override string ToString() => Value.ToString();
}