using System.IO.Abstractions;

namespace NesCs.Logic.File;

public class FileSystemProxy
{
    public class Builder
    {
        private IFileSystem _fileSystem;
        private NesFileOptions _options;

        public Builder()
        {
            _options = new NesFileOptions { LoadHeader = true, LoadTrainer = true, LoadProgramRom = true, LoadCharacterRom = true };
            _fileSystem = new FileSystem();
        }

        public Builder AccessingWith(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            return this;
        }

        public Builder Loading(NesFileOptions options)
        {
            _options = options;
            return this;
        }

        public FileSystemProxy Build() =>
            new(_fileSystem, _options);
    }

    private readonly IFileSystem _fileSystem;
    private readonly NesFileOptions _options;

    private FileSystemProxy(IFileSystem fileSystem, NesFileOptions options)
    {
        _fileSystem = fileSystem ?? throw new ArgumentException("Invalid file system", nameof(fileSystem));
        _options = options;
    }

    public INesFile Load(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Invalid filename", nameof(filename));

        if (! _fileSystem.File.Exists(filename))
        {
            throw new FileNotFoundException("Invalid filename", filename);
        }

        var contents = _fileSystem.File.ReadAllBytes(filename);
        return NesFileLoader.CreateFrom(filename, contents, _options);
    }
}