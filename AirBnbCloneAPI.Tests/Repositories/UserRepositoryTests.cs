using AirBnbCloneAPI.Models;
using AirBnbCloneAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace AirBnbCloneAPI.Tests.Repositories;

public class UserRepositoryTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly IUserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userManagerMock = MockUserManager<User>();
        _userRepository = new UserRepository(_userManagerMock.Object);
    }

    private Mock<UserManager<User>> MockUserManager<TUser>() where TUser : User
    {
        var store = new Mock<IUserStore<TUser>>();
        return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsUser_WhenUserExists()
    {
        //Arrange
        var email = "test@example.com";
        var expectedUser = new User { Id = "user123", Email = email };

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync(expectedUser);

        //Act
        var result = await _userRepository.GetByEmailAsync(email);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser, result);
        _userManagerMock.Verify(um => um.FindByEmailAsync(email), Times.Once);
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        //Arrange
        var email = "notfound@example.com";

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync((User)null);

        //Act
        var result = await _userRepository.GetByEmailAsync(email);

        //Assert
        Assert.Null(result);
    }
    
    //test GetByPhone When UserExists
    //test GetByPhone When UserDoesn'tExist

    // [Fact]
    // public async Task CreateUserAsync_ReturnsTrue_WhenUserExists()
    // {
    //     
    // }

}