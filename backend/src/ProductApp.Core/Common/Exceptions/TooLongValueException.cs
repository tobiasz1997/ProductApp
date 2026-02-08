using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class TooLongValueException(string name, int value) : BadRequestException($"{name} is too long - max {value.ToString()} characters.");