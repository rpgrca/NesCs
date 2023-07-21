using System.IO.Abstractions;

namespace NesCs.Logic;

public class FileSystemProxy
{
    public static FileSystemProxy CreateWith(IFileSystem fileSystem) => new(fileSystem);

    public static FileSystemProxy Create() => new(new FileSystem());

    private readonly IFileSystem _fileSystem;

    private FileSystemProxy(IFileSystem fileSystem) =>
        _fileSystem = fileSystem ?? throw new ArgumentException("Invalid file system", nameof(fileSystem));

    public INesFile Load(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Invalid filename", nameof(filename));

        if (! _fileSystem.File.Exists(filename))
        {
            throw new FileNotFoundException("Invalid filename", filename);
        }

        var contents = _fileSystem.File.ReadAllBytes(filename);
        return new NesFile(filename, contents);
    }
}