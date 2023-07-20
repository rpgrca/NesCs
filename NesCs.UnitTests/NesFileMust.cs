using NesCs.Logic;

namespace NesCs.UnitTests;

public class NesFileMust
{
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void ThrowException_WhenFileNameIsEmpty(string invalidName)
    {
        var exception = Assert.Throws<ArgumentException>("filename", () => new NesFile(invalidName, FileSystemWrapper.Create()));
        Assert.Contains("Invalid filename", exception.Message);
    }
}