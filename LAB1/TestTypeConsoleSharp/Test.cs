public record Test
{
    public static Test Default => new Test
    {
        Name = "!Default!",
        Date = DateTime.Now,
    };

    public string? Name { get; set; }
    public DateTime Date { get; set; }
}