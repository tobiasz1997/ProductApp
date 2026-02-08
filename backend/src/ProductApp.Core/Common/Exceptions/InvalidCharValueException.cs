using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class InvalidCharValueException(string name) : BadRequestException($"{name} must contain at least one invalid character.");