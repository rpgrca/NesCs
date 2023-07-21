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

    [Theory]
    [MemberData(nameof(NesFileHeaderFeeder))]
    public void CreateNesFile_WhenFileHasValidHeader(byte[] fileContents, int expectedProgramRomSize, int expectedCharacterRomSize, Mirroring expectedMirroring,
        bool expectedBatteryBackup, bool expectedTrainer)
    {
        var fileStub = new MockFileData(fileContents);
        var proxy = FileSystemProxy.CreateWith(new MockFileSystem(new Dictionary<string, MockFileData>() { { NES_FILENAME, fileStub } }));
        var sut = proxy.Load(NES_FILENAME);

        Assert.NotNull(sut);
        Assert.Equal(NES_FILENAME, sut.Filename);
        Assert.Equal(expectedProgramRomSize, sut.ProgramRomSize);
        Assert.Equal(expectedCharacterRomSize, sut.CharacterRomSize);
        Assert.Equal(expectedMirroring, sut.Flags6.Mirroring);
        Assert.Equal(expectedBatteryBackup, sut.Flags6.HasBatteryBackedProgramRam);
        Assert.Equal(expectedTrainer, sut.Flags6.HasTrainer);
    }

    public static IEnumerable<object[]> NesFileHeaderFeeder()
    {
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b1000011, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16, Mirroring.Vertical, true, false };
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b0111100, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16, Mirroring.Horizontal, false, true };
    }
}

public static class Constants
{
    public static byte[] GetEmptyNesHeaderFile() =>
        new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
}