using System.IO.Abstractions.TestingHelpers;
using NesCs.Logic;

namespace NesCs.UnitTests;

public class NesFileMust
{
    private const string NES_FILENAME = "test.nes";

    [Fact]
    public void ThrowException_WhenFileIsSmallerThanHeader()
    {
        var fileStub = new MockFileData(Array.Empty<byte>());
        var proxy = FileSystemProxy.CreateWith(new MockFileSystem(new Dictionary<string, MockFileData>() { { NES_FILENAME, fileStub } }));
        var exception = Assert.Throws<ArgumentException>("_contents", () => proxy.Load(NES_FILENAME));
        Assert.Contains("Could not find header", exception.Message);
    }

    [Fact]
    public void ThrowException_WhenFileDoesNotHaveCorrectSignature()
    {
        var fileStub = new MockFileData(Constants.GetEmptyNesHeaderFile());
        var proxy = FileSystemProxy.CreateWith(new MockFileSystem(new Dictionary<string, MockFileData>() { { NES_FILENAME, fileStub } }));
        var exception = Assert.Throws<ArgumentException>("_contents", () => proxy.Load(NES_FILENAME));
        Assert.Contains("Signature not found", exception.Message);
    }

    [Fact]
    public void CreateNesFile_WhenFileHasValidHeader()
    {
        var fileStub = new MockFileData(Constants.GetValidNesHeaderFile());
        var proxy = FileSystemProxy.CreateWith(new MockFileSystem(new Dictionary<string, MockFileData>() { { NES_FILENAME, fileStub } }));
        var sut = proxy.Load(NES_FILENAME);

        Assert.NotNull(sut);
        Assert.Equal(NES_FILENAME, sut.Filename);
        Assert.Equal(32, sut.ProgramRomSize);
    }
}

public static class Constants
{
    public static byte[] GetEmptyNesHeaderFile() =>
        new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

    public static byte[] GetValidNesHeaderFile() =>
        new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
}