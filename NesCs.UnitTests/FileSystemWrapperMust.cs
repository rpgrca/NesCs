using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
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

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void ThrowException_WhenFileNameIsEmpty(string invalidName)
    {
        var sut = FileSystemProxy.Create();
        var exception = Assert.Throws<ArgumentException>("filename", () => sut.Load(invalidName));
        Assert.Contains("Invalid filename", exception.Message);
    }

}

public class FileDoesNotExistFileSystem : IFileSystem
{
    public IDirectory Directory => throw new NotImplementedException();

    public IDirectoryInfoFactory DirectoryInfo => throw new NotImplementedException();

    public IDriveInfoFactory DriveInfo => throw new NotImplementedException();

    public IFile File => throw new NotImplementedException();

    public IFileInfoFactory FileInfo => throw new NotImplementedException();

    public IFileStreamFactory FileStream => throw new NotImplementedException();

    public IFileSystemWatcherFactory FileSystemWatcher => throw new NotImplementedException();

    public IPath Path => throw new NotImplementedException();
}