using ProductApp.Application.Common.Services;

namespace ProductApp.Infrastructure.Common.Time;

public class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}