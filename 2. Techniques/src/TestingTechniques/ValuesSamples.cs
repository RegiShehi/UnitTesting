namespace TestingTechniques;

public class ValuesSamples
{
    public string FullName = "Regi Shehi";

    public int Age = 30;

    public DateOnly DateOfBirth = new(1992, 6, 30);

    public User AppUser = new()
    {
        FullName = "Regi Shehi",
        Age = 30,
        DateOfBirth = new DateOnly(1992, 6, 30)
    };

    public IEnumerable<User> Users =
    [
        new()
        {
            FullName = "Regi Shehi",
            Age = 30,
            DateOfBirth = new DateOnly(1992, 6, 30)
        },
        new()
        {
            FullName = "Tom Scott",
            Age = 37,
            DateOfBirth = new DateOnly(1984, 6, 9)
        },
        new()
        {
            FullName = "Steve Mould",
            Age = 43,
            DateOfBirth = new DateOnly(1978, 10, 5)
        }
    ];

    public IEnumerable<int> Numbers = [1, 5, 10, 15];

    public event EventHandler ExampleEvent;

    internal int InternalSecretNumber = 42;

    public virtual void RaiseExampleEvent()
    {
        ExampleEvent(this, EventArgs.Empty);
    }
}