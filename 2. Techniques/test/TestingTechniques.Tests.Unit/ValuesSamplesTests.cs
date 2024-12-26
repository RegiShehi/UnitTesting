using FluentAssertions;

namespace TestingTechniques.Tests.Unit;

public class ValuesSamplesTests
{
    private readonly ValuesSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Regi Shehi");
        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Regi");
        fullName.Should().EndWith("Shehi");
    }

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(30);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20);
        age.Should().BeLessThan(40);
        age.Should().BeInRange(18, 60);
    }

    [Fact]
    public void DateAssertionExample()
    {
        var dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new DateOnly(1992, 6, 30));
        dateOfBirth.Should().BeAfter(new DateOnly(1990, 1, 1));
        dateOfBirth.Should().BeBefore(new DateOnly(2000, 1, 1));
    }

    [Fact]
    public void ObjectAssertionExample()
    {
        var expected = new User()
        {
            FullName = "Regi Shehi",
            Age = 30,
            DateOfBirth = new DateOnly(1992, 6, 30)
        };

        var user = _sut.AppUser;

        user.Should().BeEquivalentTo(expected);
        user.Should().BeOfType<User>();
        user.Should().NotBeNull();
    }

    [Fact]
    public void EnumerableObjectsAssertionExample()
    {
        var expected = new User
        {
            FullName = "Regi Shehi",
            Age = 30,
            DateOfBirth = new DateOnly(1992, 6, 30)
        };

        var users = _sut.Users.ToList();

        users.Should().ContainEquivalentOf(expected);
        users.Should().HaveCount(3);
        users.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        Action result = () => calculator.Divide(5, 0);

        result.Should().Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
    }

    [Fact]
    public void EventRaisedAssertionExample()
    {
        var monitorSubject = _sut.Monitor();

        _sut.RaiseExampleEvent();

        monitorSubject.Should().Raise("ExampleEvent");
    }
}