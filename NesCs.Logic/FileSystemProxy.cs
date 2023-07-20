using System.IO.Abstractions;

namespace NesCs.Logic;

public class FileSystemProxy
{
    public static FileSystemProxy CreateWith(IFileSystem fileSystem) => new(fileSystem);

    public static FileSystemProxy Create() => new(new FileSystem());

    private readonly IFileSystem _fileSystem;

    private FileSystemProxy(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem ?? throw new ArgumentException("Invalid file system", nameof(fileSystem));
    }
}