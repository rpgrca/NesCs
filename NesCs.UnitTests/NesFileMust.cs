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
        bool expectedBatteryBackup, bool expectedTrainer, bool expectedIgnoreMirroring, bool expectedVsUnisystem, bool expectedPlayChoice10,
        bool expectedVersionFormat)
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
        Assert.Equal(expectedIgnoreMirroring, sut.Flags6.IgnoreMirroring);
        Assert.Equal(expectedVsUnisystem, sut.Flags7.HasVsUnisystem);
        Assert.Equal(expectedPlayChoice10, sut.Flags7.HasPlayChoice10);
        Assert.Equal(expectedVersionFormat, sut.Flags7.HasVersion2Format);
    }

    public static IEnumerable<object[]> NesFileHeaderFeeder()
    {
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b10000011, 0b00000000, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16,
            Mirroring.Vertical, true, false, false,
            false, false, false };
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111111, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16,
            Mirroring.Horizontal, false, true, true,
            true, true, false };
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111011, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16,
            Mirroring.Horizontal, false, true, true,
            true, true, true };

    }
}

public static class Constants
{
    public static byte[] GetEmptyNesHeaderFile() =>
        new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
}