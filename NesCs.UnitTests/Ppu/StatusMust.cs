using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class StatusMust
{
    private static Status CreateSubjectUnderTest() =>
        new(new RamController.Builder().WithRamOf(new byte[0x2100]).Build(), new ByteToggle(), new PpuIOBus(new Clock(0)));

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

    [Fact]
    public void ResetVerticalBlankStart_WhenRead()
    {
        var sut = CreateSubjectUnderTest();
        sut.V = 1;
        _ = sut.V;
        Assert.Equal(0, sut.V);
    }

    [Fact]
    public void ResetToggler_WhenFlagVsyncIsRead()
    {
        var spy = new ByteToggleSpy();
        var sut = CreateSubjectUnderTest(spy);
        _ = sut.V;
        Assert.True(spy.Toggled);
    }

    private static Status CreateSubjectUnderTest(IByteToggle spy) =>
        new(new RamController.Builder().WithRamOf(new byte[0x2100]).Build(), spy, new PpuIOBus(new Clock(0)));

    [Fact]
    public void ResetToggler_WhenOpenBusIsRead()
    {
        var spy = new ByteToggleSpy();
        var sut = CreateSubjectUnderTest(spy);
        _ = sut.O;
        Assert.True(spy.Toggled);
    }

    [Fact]
    public void ResetToggler_WhenFlagSIsRead()
    {
        var spy = new ByteToggleSpy();
        var sut = CreateSubjectUnderTest(spy);
        _ = sut.S;
        Assert.True(spy.Toggled);
    }

    [Fact]
    public void ResetToggler_WhenFlagIsRead()
    {
        var spy = new ByteToggleSpy();
        var sut = CreateSubjectUnderTest(spy);
        _ = sut.Read();
        Assert.True(spy.Toggled);
    }


    [Theory]
    [InlineData(0x00, 0)]
    [InlineData(0x00, 1)]
    [InlineData(0xFF, 0)]
    [InlineData(0xFF, 1)]
    public void MaintainVerticalBlank_WhenWritingTo2002(byte writtenValue, byte expectedResult)
    {
        var sut = CreateSubjectUnderTest();
        sut.V = expectedResult;
        sut.Write(writtenValue);
        Assert.Equal(expectedResult, sut.V);
    }
}