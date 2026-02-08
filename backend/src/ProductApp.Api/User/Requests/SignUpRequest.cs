using System.ComponentModel.DataAnnotations;

namespace ProductApp.Api.User.Requests;

public class SignUpRequest
{
    [property: Required] 
    public string Login { get; init; } = string.Empty;
    [property: Required] 
    public string Password { get; init; } = string.Empty;
};