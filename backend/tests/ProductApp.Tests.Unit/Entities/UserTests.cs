using ProductApp.Core.Common.Exceptions;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.ValueObjects;
using Shouldly;
using Xunit;

namespace ProductApp.Tests.Unit.Entities;

public class UserTests
{
    [Fact]
    public void user_given_correct_data_should_success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var login = "Login12345";
        var passwordHash = "password123456";
        var createdAt = new DateTime(2020, 01, 01);
        
        // Act
        var result = new User(id, login, passwordHash, createdAt);
        
        //Asset
        result.ShouldNotBeNull();
        result.Id.Value.ShouldBe(id);
        result.Login.Value.ShouldBe(login);
        result.PasswordHash.Value.ShouldBe(passwordHash);
        result.CreatedAt.ShouldBe(createdAt);
    }
    
    [Fact]
    public void login_given_correct_data_should_success()
    {
        // Arrange
        var login = "Random1234";
        
        // Act
        var result = new Login(login);
        
        //Asset
        result.ShouldNotBeNull();
        result.Value.ShouldBe(login);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void login_given_incorrect_data_empty_or_null_should_fail(string value)
    {
        // Act
        var exception = Record.Exception(() => new Login(value));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyValueException>();
        exception.Message.ShouldBe("Login is empty.");
    }
    
    [Theory]
    [InlineData("Log12")]
    [InlineData("Login12345Login12345Login12345Login12345Login12345")]
    public void login_given_correct_data_min_max_length_should_success(string value)
    {
        // Act
        var result = new Login(value);
        
        //Asset
        result.ShouldNotBeNull();
        result.Value.ShouldBe(value);
    }
    
    [Fact]
    public void login_given_incorrect_data_min_length_should_fail()
    {
        // Arrange
        var login = "Ra12";
        
        // Act
        var exception = Record.Exception(() => new Login(login));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TooShortValueException>();
        exception.Message.ShouldBe("Login is too short - min 5 characters.");
    }
    
    [Fact]
    public void login_given_incorrect_data_max_length_should_fail()
    {
        // Arrange
        var login = "Login12345Login12345Login12345Login12345Login123456";
        
        // Act
        var exception = Record.Exception(() => new Login(login));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TooLongValueException>();
        exception.Message.ShouldBe("Login is too long - max 50 characters.");
    }
    
    [Fact]
    public void login_given_incorrect_data_no_uppercase_letter_should_fail()
    {
        // Arrange
        var login = "random1234";
        
        // Act
        var exception = Record.Exception(() => new Login(login));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UpperLetterValueException>();
        exception.Message.ShouldBe("Login must contain uppercase letter.");
    }
    
    [Fact]
    public void login_given_incorrect_data_no_lowercase_letter_should_fail()
    {
        // Arrange
        var login = "RANDOM1234";
        
        // Act
        var exception = Record.Exception(() => new Login(login));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<LowerLetterValueException>();
        exception.Message.ShouldBe("Login must contain lowercase letter.");
    }
    
    [Fact]
    public void login_given_incorrect_data_no_digit_should_fail()
    {
        // Arrange
        var login = "RANDOMrandom";
        
        // Act
        var exception = Record.Exception(() => new Login(login));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<NoDigitValueException>();
        exception.Message.ShouldBe("Login must contain at least one number.");
    }
    
    [Theory]
    [InlineData("Random1234Ą")]
    [InlineData("Random1234ą")]
    [InlineData("Random1234ó")]
    [InlineData("Random1234Ź")]
    [InlineData("Random1234!")]
    [InlineData("Random1234@")]
    [InlineData("Random1234/")]
    [InlineData("Random1234?")]
    [InlineData("Random1234;")]
    public void login_given_incorrect_data_invalid_character_should_fail(string value)
    {
        // Act
        var exception = Record.Exception(() => new Login(value));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCharValueException>();
        exception.Message.ShouldBe($"{value} must contain at least one invalid character.");
    }
    
    [Fact]
    public void password_given_correct_data_should_success()
    {
        // Arrange
        var password = "Random123456";
        
        // Act
        var result = new Password(password);
        
        //Asset
        result.ShouldNotBeNull();
        result.Value.ShouldBe(password);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void password_given_incorrect_data_empty_or_null_should_fail(string value)
    {
        // Act
        var exception = Record.Exception(() => new Password(value));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyValueException>();
        exception.Message.ShouldBe("Password is empty.");
    }
    
    [Theory]
    [InlineData("Password1234")]
    [InlineData("Password1234Password1234Password1234Password1234Password12345678")]
    public void password_given_correct_data_min_max_length_should_success(string value)
    {
        // Act
        var result = new Password(value);
        
        //Asset
        result.ShouldNotBeNull();
        result.Value.ShouldBe(value);
    }
    
    [Fact]
    public void password_given_incorrect_data_min_length_should_fail()
    {
        // Arrange
        var password = "Password123";
        
        // Act
        var exception = Record.Exception(() => new Password(password));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TooShortValueException>();
        exception.Message.ShouldBe("Password is too short - min 12 characters.");
    }
    
    [Fact]
    public void password_given_incorrect_data_max_length_should_fail()
    {
        // Arrange
        var password = "Password1234Password1234Password1234Password1234Password123456789";
        
        // Act
        var exception = Record.Exception(() => new Password(password));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TooLongValueException>();
        exception.Message.ShouldBe("Password is too long - max 64 characters.");
    }
    
    [Fact]
    public void password_hash_given_correct_data_should_success()
    {
        // Arrange
        var password = "Random123456";
        
        // Act
        var result = new PasswordHash(password);
        
        //Asset
        result.ShouldNotBeNull();
        result.Value.ShouldBe(password);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void password_hash_given_incorrect_data_empty_or_null_should_fail(string value)
    {
        // Act
        var exception = Record.Exception(() => new PasswordHash(value));
        
        //Asset
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InternalException>();
        exception.Message.ShouldBe("Internal application error.");
    }
}