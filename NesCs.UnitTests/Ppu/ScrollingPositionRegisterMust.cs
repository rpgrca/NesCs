using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class ScrollingPositionRegisterMust
{
    [Fact]
    public void InitializePositionCorrectly()
    {
        var ramController = new RamControllerSpy();
        var sut = new Logic.Ppu.ScrollingPositionRegister(ramController);
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
        var sut = new Logic.Ppu.ScrollingPositionRegister(ramController);
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
        var sut = new Logic.Ppu.ScrollingPositionRegister(ramController);
        sut.Write(0x4);
        sut.Write(value);
        Assert.Equal(4, sut.CameraPositionX);
        Assert.Equal(value, sut.CameraPositionY);
    }
/*
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
    }*/

    /*[Fact]
    public void ChangeAddressToggle_WhenWritingScroll()
    {
        var vram = Enumerable.Range(1, 0x3FFF).Select(p => (byte)(p / 64)).ToArray();
        var ramController = new RamController.Builder().Build();
        var sut = new Logic.Ppu.Ppu2C02.Builder().WithVram(vram).WithRamController(ramController).Build();
        sut.PpuAddr.Write(0x25);
        sut.PpuAddr.Write(0x93);
        sut.PpuScroll.Write(0xF0);
        sut.PpuAddr.Write(0xDE);
        var value = sut.PpuData.Read();
        value = sut.PpuData.Read();
        Assert.Equal(75, value);

    }*/
}