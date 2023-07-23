using System.IO.Abstractions.TestingHelpers;
using NesCs.Logic.File;

namespace NesCs.UnitTests;

public class FileSystemWrapperMust
{
    [Fact]
    public void ThrowException_WhenCreatingFileSystemWrapperWithNullFileSystem()
    {
        var exception = Assert.Throws<ArgumentException>("fileSystem", () => FileSystemProxy.CreateWith(null));
        Assert.Contains("Invalid file system", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void ThrowException_WhenFileNameIsEmpty(string invalidName)
    {
        var sut = FileSystemProxy.Create();
        var exception = Assert.Throws<ArgumentException>("filename", () => sut.Load(invalidName));
        Assert.Contains("Invalid filename", exception.Message);
    }

    [Fact]
    public void ThrowException_WhenLoadingFileThatDoesNotExist()
    {
        var fileSystemStub = new MockFileSystem(new Dictionary<string, MockFileData>() {});
        var sut = FileSystemProxy.CreateWith(fileSystemStub);
        var exception = Assert.Throws<FileNotFoundException>(() => sut.Load("subject.nes"));
        Assert.Contains("Invalid filename", exception.Message);
    }
}