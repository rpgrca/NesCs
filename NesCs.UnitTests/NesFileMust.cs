using NesCs.Logic;

namespace NesCs.UnitTests;

public class NesFileMust
{
    [Fact]
    public void ThrowException_WhenFileNameIsEmpty()
    {
        var exception = Assert.Throws<ArgumentException>("filename", () => new NesFile(""));
        Assert.Contains("Invalid filename", exception.Message);
    }
}