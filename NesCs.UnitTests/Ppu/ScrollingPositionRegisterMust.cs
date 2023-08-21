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
        var vram = PopulateVram();
        var ramController = new RamController.Builder().Build();
        var sut = new Ppu2C02.Builder().WithVram(vram).WithRamController(ramController).Build();
        sut.PpuAddr.Write(0x25);
        sut.PpuAddr.Write(0x6A);
        sut.PpuScroll.Write(0x55);
        sut.PpuScroll.Write(0x55);
        _ = sut.PpuData.Read();
        var value = sut.PpuData.Read();
        Assert.Equal(0xEA, value);
    }

    private byte[] PopulateVram()
    {
        var vram = new byte[0x4000];
        for (var index = 0; index < 0x4000; index++)
        {
            vram[index] = index switch
            {
                >= 0x2400 and <= 0x24FF => (byte)(index - 0x2400 + 0x40),
                >= 0x2500 and <= 0x25FF => (byte)(index - 0x2500 + 0x80),
                >= 0x2600 and <= 0x26FF => (byte)(index - 0x2600 + 0xC0),
                >= 0x2700 and <= 0x2BFF => (byte)(index - 0x2700 + 0x00),
                _ => 0
            };
        }

        return vram;
    }
}