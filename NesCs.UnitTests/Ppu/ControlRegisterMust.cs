using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class ControlRegisterMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void SetBaseNametableAddressBitsCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.N = value;

        Assert.Equal(value, sut.N);
    }

    private static ControlRegister CreateSubjectUnderTest() =>
        new(new RamControllerSpy { Ram = new byte[0x2100] });

    [Theory]
    [InlineData(0, 0x2000)]
    [InlineData(1, 0x2400)]
    [InlineData(2, 0x2800)]
    [InlineData(3, 0x2C00)]
    public void CalculateBaseNametableAddressCorrectly(byte value, int expectedAddress)
    {
        var sut = CreateSubjectUnderTest();
        sut.N = value;
        Assert.Equal(expectedAddress, sut.GetBaseNametableAddress());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetVramAddressIncrementCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.I = value;
        Assert.Equal(value, sut.I);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpritePatternTableAddressCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.S = value;
        Assert.Equal(value, sut.S);
    }

    [Theory]
    [InlineData(0, 0x0000)]
    [InlineData(1, 0x1000)]
    public void CalculateSpritePatternTableAddressCorrectly(byte value, int expectedAddress)
    {
        var sut = CreateSubjectUnderTest();
        sut.S = value;
        Assert.Equal(expectedAddress, sut.GetSpritePatternTableAddress());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetBackgroundPatternTableAddressCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.B = value;
        Assert.Equal(value, sut.B);
    }

    [Theory]
    [InlineData(0, 0x0000)]
    [InlineData(1, 0x1000)]
    public void CalculateBackgroundPatternTableAddressCorrectly(byte value, int expectedAddress)
    {
        var sut = CreateSubjectUnderTest();
        sut.B = value;
        Assert.Equal(expectedAddress, sut.GetBackgroundPatternTableAddress());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpriteSizeCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.H = value;
        Assert.Equal(value, sut.H);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetPpuMasterSlaveCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.P = value;
        Assert.Equal(value, sut.P);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetNmiAtStartOfVerticalBlankingIntervalCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.V = value;

        Assert.Equal(value, sut.V);
    }
}

public class RamControllerSpy : IRamController
{
    public byte[] Ram { get; set; }

    public byte this[int index] { get => Ram[index]; set => Ram[index] = value; }

    public void Copy(byte[] program, int startIndex, int memoryOffset, int programSize)
    {
    }
}