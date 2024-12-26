using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Users.Api.Contracts;
using Users.Api.Controllers;
using Users.Api.Mappers;
using Users.Api.Models;
using Users.Api.Services;

namespace Users.Api.Tests.Unit;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly IUserService _userService = Substitute.For<IUserService>();

    public UserControllerTests()
    {
        _sut = new UserController(_userService);
    }

    [Fact]
    public async Task GetById_ReturnsOkAndObject_WhenUserExists()
    {
        // arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe"
        };

        _userService.GetByIdAsync(user.Id).Returns(user);
        var userResponse = user.ToUserResponse();

        // act
        var result = (OkObjectResult)await _sut.GetById(user.Id);

        // assert
        result.Should().BeOfType<OkObjectResult>();
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(userResponse);
        result.Value.Should().BeOfType<UserResponse>();
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // arrange
        _userService.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        // act
        var result = (NotFoundResult)await _sut.GetById(Guid.NewGuid());

        // assert
        result.Should().BeOfType<NotFoundResult>();
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoUsersExists()
    {
        // arrange
        _userService.GetAllAsync().Returns([]);

        // act
        var result = (OkObjectResult)await _sut.GetAll();

        // assert
        result.Should().BeOfType<OkObjectResult>();
        result.Value.As<IEnumerable<UserResponse>>().Should().BeEmpty();
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAll_ReturnsUsersResponse_WhenUsersExists()
    {
        // arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe"
        };

        var users = new[] { user };

        _userService.GetAllAsync().Returns(users);
        var usersResponse = users.Select(x => x.ToUserResponse());

        // act
        var result = (OkObjectResult)await _sut.GetAll();

        // assert
        result.Should().BeOfType<OkObjectResult>();
        result.Value.As<IEnumerable<UserResponse>>().Should().BeEquivalentTo(usersResponse);
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenCreateUserRequestIsValid()
    {
        // Arrange
        var createUserRequest = new CreateUserRequest
        {
            FullName = "Regi Shehi"
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = createUserRequest.FullName
        };

        _userService.CreateAsync(Arg.Do<User>(x => user = x)).Returns(true);

        // Act
        var result = (CreatedAtActionResult)await _sut.Create(createUserRequest);

        // Assert
        var expectedUserResponse = user.ToUserResponse();

        result.StatusCode.Should().Be(201);
        result.Value.As<UserResponse>().Should().BeEquivalentTo(expectedUserResponse);
        result.RouteValues!["id"].Should().BeEquivalentTo(user.Id);

        // result.Value.As<UserResponse>().Should()
        //     .BeEquivalentTo(expectedUserResponse, options => options.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenCreateUserRequestIsInvalid()
    {
        // Arrange
        _userService.CreateAsync(Arg.Any<User>()).Returns(false);

        // Act
        var result = (BadRequestResult)await _sut.Create(new CreateUserRequest());

        // Assert
        result.StatusCode.Should().Be(400);
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task DeleteById_ReturnsOk_WhenUserWasDeleted()
    {
        // Arrange
        _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(true);

        // Act
        var result = (OkResult)await _sut.DeleteById(Guid.NewGuid());

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteById_ReturnsNotFound_WhenUserWasNotDeleted()
    {
        // Arrange
        _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(false);

        // Act
        var result = (NotFoundResult)await _sut.DeleteById(Guid.NewGuid());

        // Assert
        result.StatusCode.Should().Be(404);
    }
}