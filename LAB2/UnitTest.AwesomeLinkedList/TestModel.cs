using System;

public record TestModel
{
    public static TestModel Default => new TestModel
    {
        Name = "!Default!",
        Date = DateTime.Now,
    };

    public string? Name { get; set; }
    public DateTime Date { get; set; }
}