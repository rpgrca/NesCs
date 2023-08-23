using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class MaskMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetGrayscaleCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.Grey = value;
        Assert.Equal(value, sut.Grey);
    }

    private static Mask CreateSubjectUnderTest() =>
        new(new RamController.Builder().WithRamOf(new byte[0x2100]).Build(), new PpuIOBus(new Clock(0)));

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetBackgroundInLeftmostPixelsCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.Lm = value;
        Assert.Equal(value, sut.Lm);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpritesFlagCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.M = value;
        Assert.Equal(value, sut.M);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetShowBackgroundFlagCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.Lb = value;
        Assert.Equal(value, sut.Lb);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetShowSpritesFlagCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.Ls = value;
        Assert.Equal(value, sut.Ls);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetEmphasizeRedFlagCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.R = value;
        Assert.Equal(value, sut.R);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetEmphasizeGreenFlagCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.G = value;
        Assert.Equal(value, sut.G);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetEmphasizeBlueFlagCorrectly(byte value)
    {
        var sut = CreateSubjectUnderTest();
        sut.B = value;
        Assert.Equal(value, sut.B);
    }
}