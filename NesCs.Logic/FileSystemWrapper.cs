using System.IO.Abstractions;

namespace NesCs.Logic;

public class FileSystemWrapper
{
    private readonly IFileSystem _fileSystem;

    public static FileSystemWrapper Create() => new(new FileSystem());

    public static FileSystemWrapper CreateWith(IFileSystem fileSystem) => new(fileSystem);

    private FileSystemWrapper(IFileSystem fileSystem) =>
        _fileSystem = fileSystem ?? throw new ArgumentException("Invalid file system", nameof(fileSystem));
}