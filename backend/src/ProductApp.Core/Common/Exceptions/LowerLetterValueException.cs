using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class LowerLetterValueException(string name) : BadRequestException($"{name} must contain lowercase letter.");