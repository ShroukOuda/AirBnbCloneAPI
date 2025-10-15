using AirBnbCloneAPI.Dtos;
using AirBnbCloneAPI.Models;
using AirBnbCloneAPI.Repositories;
using AirBnbCloneAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AirBnbCloneAPI.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock =  new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _authService = new AuthService(_userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task RegisterAsync_ReturnFalse_WhenCountryCodeInValid()
    {
        //Arrange
        var model = new RegisterDto()
        {
            FirstName = "Shrouq",
            LastName = "Ouda",
            DateOfBirth = DateTime.Now,
            UserName = "ShrouqOuda",
            CountryCode = "eg",
            PhoneNumber = "+20123456789",
            Email = "test@example.com",
            Password = "Pass1234!"
        };
        
        //Act
        var result = await _authService.RegisterAsync(model);
        
        //Assert
        Assert.False(result.Success);
        Assert.Equal("Country code must be exactly 2 uppercase letters.", result.Message);
    }

    [Fact]
    public async Task RegisterAsync_ReturnFalse_WhenPhoneNumberInvalidForSelectedCountry()
    {
        //Arrange
        var model = new RegisterDto()
        {
            FirstName = "Shrouq",
            LastName = "Ouda",
            DateOfBirth = DateTime.Now,
            UserName = "ShrouqOuda",
            CountryCode = "EG",
            PhoneNumber = "+10123456789",
            Email = "test@example.com",
            Password = "Pass1234!"
        };
        
        //Act
        var result = await _authService.RegisterAsync(model);
        
        //Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid phone number for the selected country.", result.Message);
    }

    [Fact]
    public async Task RegisterAsync_ReturnFalse_WhenEmailAlreadyExists()
    {
        //arrange
        var model = new RegisterDto()
        {
            FirstName = "Shrouq",
            LastName = "Ouda",
            DateOfBirth = DateTime.Now,
            UserName = "ShrouqOuda",
            CountryCode = "EG",
            PhoneNumber = "+201011223344",
            Email = "test@example.com",
            Password = "Pass1234!"
        };

        _userRepositoryMock.Setup(r => r.GetByEmailAsync(model.Email))
            .ReturnsAsync(new User { Email = model.Email });
        
        //Act
        var result = await _authService.RegisterAsync(model);
        
        //Assert
        Assert.False(result.Success);
        Assert.Equal("Email already exists.", result.Message);
    }

    [Fact]
    public async Task RegisterAsync_ReturnFalse_WhenPhoneAlreadyExists()
    {
        //arrange
        var model = new RegisterDto()
        {
            FirstName = "Shrouq",
            LastName = "Ouda",
            DateOfBirth = DateTime.Now,
            UserName = "ShrouqOuda",
            CountryCode = "EG",
            PhoneNumber = "+201011223344",
            Email = "test@example.com",
            Password = "Pass1234!"
        };
        
        _userRepositoryMock.Setup(r => r.GetByPhoneAsync(model.PhoneNumber))
            .ReturnsAsync(new User { PhoneNumber = model.PhoneNumber });
        
        //act
        var result = await _authService.RegisterAsync(model);
        
        //assert
        Assert.False(result.Success);
        Assert.Equal("Phone number already exists.", result.Message);
    }

    [Fact]
    public async Task RegisterAsync_ReturnTrue_WhenRegistrationSucceeds()
    {
        //arrange
        var model = new RegisterDto()
        {
            FirstName = "Shrouq",
            LastName = "Ouda",
            DateOfBirth = DateTime.Now,
            UserName = "ShrouqOuda",
            CountryCode = "EG",
            PhoneNumber = "+201011223344",
            Email = "test@example.com",
            Password = "Pass1234!"
        };
        
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(model.Email))
            .ReturnsAsync((User)null);
        _userRepositoryMock.Setup(r => r.GetByPhoneAsync(model.PhoneNumber))
            .ReturnsAsync((User)null);
        _userRepositoryMock.Setup(r => r.CreateUserAsync(It.IsAny<User>(), model.Password))
            .ReturnsAsync(IdentityResult.Success);
        _mapperMock.Setup(m => m.Map<User>(It.IsAny<RegisterDto>()))
            .Returns(new User { Email = model.Email, PhoneNumber = model.PhoneNumber });
        
        //act
        var result =  await _authService.RegisterAsync(model);
        
        //assert
        Assert.True(result.Success);
        Assert.Equal("User Registered Successfully.", result.Message);
    }
}