using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class StatusMust
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(0b11111100, 0b11100)]
    [InlineData(0b11110000, 0b10000)]
    public void SetOpenBusCorrectly(byte value, byte expectedValue)
    {
        var sut = CreateSubjectUnderTest();
        sut.OpenBus = value;
        Assert.Equal(expectedValue, sut.OpenBus);
    }

    private static Status CreateSubjectUnderTest() =>
        new(new RamController.Builder().WithRamOf(new byte[0x2100]).Build(), new ByteToggle());

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpriteOverflowCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.O = value;
        Assert.Equal(value, sut.O);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpriteZeroHitCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.S = value;
        Assert.Equal(value, sut.S);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetVerticalBlankStartCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.V = value;
        Assert.Equal(value, sut.V);
    }
}