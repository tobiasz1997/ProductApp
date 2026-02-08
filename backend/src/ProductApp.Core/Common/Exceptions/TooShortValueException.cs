using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class TooShortValueException(string name, int value) : BadRequestException($"{name} is too short - min {value.ToString()} characters.");