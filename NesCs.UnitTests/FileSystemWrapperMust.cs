using NesCs.Logic;

namespace NesCs.UnitTests;

public class FileSystemWrapperMust
{
    [Fact]
    public void ThrowException_WhenCreatingFileSystemWrapperWithNullFileSystem()
    {
        var exception = Assert.Throws<ArgumentException>("fileSystem", () => FileSystemProxy.CreateWith(null));
        Assert.Contains("Invalid file system", exception.Message);
    }
}