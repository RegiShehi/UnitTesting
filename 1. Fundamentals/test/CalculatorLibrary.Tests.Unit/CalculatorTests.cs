using FluentAssertions;
using Xunit.Abstractions;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests : IDisposable
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly Calculator _sut = new();

    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;

        _outputHelper.WriteLine("Hello from constructor");
    }

    [Theory]
    [InlineData(5, 4, 9)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, -5, -10)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // arrange

        // act
        var result = _sut.Add(a, b);

        // assert
        // Assert.Equal(expected, result);
        result.Should().Be(expected);
    }


    [Fact(Skip = "This is just a test to show how to skip a test case")]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // arrange

        // act
        var result = _sut.Subtract(5, 4);

        // assert
        // Assert.Equal(1, result);
        result.Should().Be(1);
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("Hello from cleanup");
    }
}