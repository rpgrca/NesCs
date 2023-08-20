using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class ScrollingPositionRegisterMust
{
    [Fact]
    public void InitializePositionCorrectly()
    {
        var ramController = new RamControllerSpy();
        var toggle = new ByteToggle();
        var sut = new ScrollingPositionRegister(ramController, toggle);
        Assert.Equal(0, sut.CameraPositionX);
        Assert.Equal(0, sut.CameraPositionY);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetPositionCorrectly(byte value)
    {
        var ramController = new RamControllerSpy();
        var toggle = new ByteToggle();
        var sut = new ScrollingPositionRegister(ramController, toggle);
        sut.Write(value);
        Assert.Equal(value, sut.CameraPositionX);
        Assert.Equal(0, sut.CameraPositionY);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetPositionYCorrectly(byte value)
    {
        var ramController = new RamControllerSpy();
        var toggle = new ByteToggle();
        var sut = new ScrollingPositionRegister(ramController, toggle);
        sut.Write(0x4);
        sut.Write(value);
        Assert.Equal(4, sut.CameraPositionX);
        Assert.Equal(value, sut.CameraPositionY);
    }

    [Fact]
    public void NotChangeAddressUsedByAddress_WhenWritingScroll()
    {
        var vram = Enumerable.Range(1, 0x2100).Select(p => (byte)(p / 35)).ToArray();
        var ramController = new RamController.Builder().Build();
        var sut = new Logic.Ppu.Ppu2C02.Builder().WithVram(vram).WithRamController(ramController).Build();
        sut.PpuAddr.Write(0x25);
        sut.PpuAddr.Write(0x4B);
        sut.PpuScroll.Write(0x55);
        var value = sut.PpuAddr.Read();
        Assert.Equal(75, value);
    }

    [Fact]
    public void ChangeAddressToggle_WhenWritingScroll()
    {
        var vram = Enumerable.Range(1, 0x3FFF).Select(p => (byte)(p / 64)).ToArray();
        var ramController = new RamController.Builder().Build();
        var sut = new Ppu2C02.Builder().WithVram(vram).WithRamController(ramController).Build();
        sut.PpuAddr.Write(0x25);
        sut.PpuAddr.Write(0x93);
        sut.PpuScroll.Write(0x30);
        sut.PpuAddr.Write(0x40);
        Assert.Equal(0x2540, sut.PpuAddr.CurrentAddress);
    }

    [Fact]
    public void NotChangeAddressUsedByAddress_WhenWritingToScrollTwice()
    {
        var vram = Enumerable.Range(0, 0xff).Select(p => (byte)p).ToArray();
        var ramController = new RamController.Builder().Build();
        var sut = new Ppu2C02.Builder().WithVram(vram).WithRamController(ramController).Build();
        sut.PpuAddr.Write(0x00);
        sut.PpuAddr.Write(0x01);
        sut.PpuScroll.Write(0x55);
        sut.PpuScroll.Write(0x8C);
        _ = sut.PpuData.Read();
        var value = sut.PpuData.Read();
        Assert.Equal(1, value);
    }
}