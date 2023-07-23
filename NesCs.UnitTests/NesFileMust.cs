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
        var exception = Assert.Throws<ArgumentException>("contents", () => proxy.Load(NES_FILENAME));
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
        bool expectedBatteryBackup, bool expectedTrainer, bool expectedIgnoreMirroring, ConsoleType expectedConsoleType,
        bool expectedVersionFormat, int expectedMapperNumber, int expectedProgramRamSize, int expectedTvSystem)
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
        Assert.Equal(expectedConsoleType, sut.Flags7.ConsoleType);
        Assert.Equal(expectedVersionFormat, sut.Flags7.HasVersion2Format);
        Assert.Equal(expectedMapperNumber, sut.MapperNumber);
        Assert.Equal(expectedProgramRamSize, sut.Flags8.ProgramRamSize);
        Assert.Equal(expectedTvSystem, sut.Flags9.TvSystem);
    }

    public static IEnumerable<object[]> NesFileHeaderFeeder()
    {
        /* Original iNES */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b10000011, 0b00000000, 0x00, 0b0, 0b0, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16,
             /* Flags6  */ Mirroring.Vertical, true, false, false,
             /* Flags7  */ ConsoleType.NesOrFamicom, false, 0b1000,
             /* Flags8  */ 0,
             /* Flags9  */ 0 };

        /* Archaic iNES */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b10000011, 0b00000000, 0x00, 0b0, 0b0, 0x00, 0x01, 0x02, 0x04, 0x05 }, 32, 16,
             /* Flags6  */ Mirroring.Vertical, true, false, false,
             /* Flags7  */ ConsoleType.NesOrFamicom, false, 0,
             /* Flags8  */ 0,
             /* Flags9  */ 0 };

        /* Archaic iNES */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b00010111, 0x20, 0b1, 0b110010, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16,
            /* Flags6  */ Mirroring.Horizontal, false, true, true,
            /* Flags7  */ ConsoleType.NesOrFamicom, false, 0,
            /* Flags8  */ 0,
            /* Flags9  */ 0 };
    }

    [Theory]
    [MemberData(nameof(Nes20FileHeaderFeeder))]
    public void CreateNesFile_WhenFileHasValidNes20Head(byte[] fileContents, int expectedProgramRomSize, int expectedCharacterRomSize, Mirroring expectedMirroring,
        bool expectedBatteryBackup, bool expectedTrainer, bool expectedIgnoreMirroring, ConsoleType expectedConsoleType,
        bool expectedVersionFormat, int expectedMapperNumber, int expectedProgramRamSize, int expectedTvSystem, int expectedTvSystemExtended,
        bool expectedProgramRam, bool expectedBusConflicts, ConsoleType expectedExtendedConsoleType)
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
        Assert.Equal(expectedConsoleType, sut.Flags7.ConsoleType);
        Assert.Equal(expectedVersionFormat, sut.Flags7.HasVersion2Format);
        Assert.Equal(expectedMapperNumber, sut.MapperNumber);
        Assert.Equal(expectedProgramRamSize, sut.Flags8.ProgramRamSize);
        Assert.Equal(expectedTvSystem, sut.Flags9.TvSystem);
        Assert.Equal(expectedTvSystemExtended, sut.Flags10.TvSystem);
        Assert.Equal(expectedProgramRam, sut.Flags10.HasProgramRam);
        Assert.Equal(expectedBusConflicts, sut.Flags10.HasBusConflicts);
        Assert.Equal(expectedExtendedConsoleType, sut.Byte13.ExtendedConsoleType);
    }

    public static IEnumerable<object[]> Nes20FileHeaderFeeder()
    {
        /* Nes20 */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111000, 0x10, 0b1, 0b110001, 0x00, 0x00, 0x00, 0x00, 0x00 }, 32, 16,
            /* Flags6  */ Mirroring.Horizontal, false, true, true,
            /* Flags7  */ ConsoleType.NesOrFamicom, true, 0b11110111,
            /* Flags8  */ 16,
            /* Flags9  */ 1,
            /* Flags10 */ 1, true, true,
            /* Byte13  */ ConsoleType.NesOrFamicom };

        /* Nes20 */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111001, 0x10, 0b1, 0b110001, 0x00, 0x00, 0b01, 0x00, 0x00 }, 32, 16,
            /* Flags6  */ Mirroring.Horizontal, false, true, true,
            /* Flags7  */ ConsoleType.NintendoVersusSystem, true, 0b11110111,
            /* Flags8  */ 16,
            /* Flags9  */ 1,
            /* Flags10 */ 1, true, true,
            /* Byte13  */ ConsoleType.NintendoVersusSystem };

        /* Nes20 */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111010, 0x10, 0b1, 0b110001, 0x00, 0x00, 0b10, 0x00, 0x00 }, 32, 16,
            /* Flags6  */ Mirroring.Horizontal, false, true, true,
            /* Flags7  */ ConsoleType.NintendoPlaychoice10, true, 0b11110111,
            /* Flags8  */ 16,
            /* Flags9  */ 1,
            /* Flags10 */ 1, true, true,
            /* Byte13  */ ConsoleType.NintendoPlaychoice10 };

        /* Nes20 */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111011, 0x10, 0b1, 0b110001, 0x00, 0x00, 0b11, 0x00, 0x00 }, 32, 16,
            /* Flags6  */ Mirroring.Horizontal, false, true, true,
            /* Flags7  */ ConsoleType.RegularFamicloneWithDecimalMode, true, 0b11110111,
            /* Flags8  */ 16,
            /* Flags9  */ 1,
            /* Flags10 */ 1, true, true,
            /* Byte13  */ ConsoleType.RegularFamicloneWithDecimalMode };

        /* Nes20 */
        yield return new object[] { new byte[] { 0x4e, 0x45, 0x53, 0x1A, 0x20, 0x10, 0b01111100, 0b11111011, 0x10, 0b1, 0b110001, 0x00, 0x00, 0b1001, 0x00, 0x00 }, 32, 16,
            /* Flags6  */ Mirroring.Horizontal, false, true, true,
            /* Flags7  */ ConsoleType.RegularFamicloneWithDecimalMode, true, 0b11110111,
            /* Flags8  */ 16,
            /* Flags9  */ 1,
            /* Flags10 */ 1, true, true,
            /* Byte13  */ ConsoleType.VT32 };
    }
}