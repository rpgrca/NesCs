using System.IO.Abstractions;

namespace NesCs.Logic;

public class FileSystemProxy
{
    public class Builder
    {
        private IFileSystem _fileSystem;
        private string _filename;

        public Builder()
        {
            _fileSystem = new FileSystem();
            _filename = string.Empty;
        }

        public Builder UsingAsFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            return this;
        }

        public Builder Loading(string filename)
        {
            _filename = filename;
            return this;
        }

        public FileSystemProxy Build() => new(_fileSystem, _filename);
    }

    private readonly IFileSystem _fileSystem;
    private readonly string _filename;

    private FileSystemProxy(IFileSystem fileSystem, string filename)
    {
        _fileSystem = fileSystem ?? throw new ArgumentException("Invalid file system", nameof(fileSystem));
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Invalid filename", nameof(filename));

        _filename = filename;
    }
}