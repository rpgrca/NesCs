using System.IO.Abstractions.TestingHelpers;
using NesCs.Logic.File;

namespace NesCs.UnitTests;

public class FileSystemWrapperMust
{
    [Fact]
    public void ThrowException_WhenCreatingFileSystemWrapperWithNullFileSystem()
    {
        var exception = Assert.Throws<ArgumentException>("fileSystem", () => new FileSystemProxy.Builder().AccessingWith(null).Build());
        Assert.Contains("Invalid file system", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void ThrowException_WhenFileNameIsEmpty(string invalidName)
    {
        var sut = new FileSystemProxy.Builder().Build();
        var exception = Assert.Throws<ArgumentException>("filename", () => sut.Load(invalidName));
        Assert.Contains("Invalid filename", exception.Message);
    }

    [Fact]
    public void ThrowException_WhenLoadingFileThatDoesNotExist()
    {
        var fileSystemStub = new MockFileSystem(new Dictionary<string, MockFileData>() {});
        var sut = new FileSystemProxy.Builder().AccessingWith(fileSystemStub).Build();
        var exception = Assert.Throws<FileNotFoundException>(() => sut.Load("subject.nes"));
        Assert.Contains("Invalid filename", exception.Message);
    }
}