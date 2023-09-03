using NesCs.Logic.Cpu.Clocking;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class OamDataPortMust
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(0xff, 0b11100011 )]
    public void NotSetBits2To4_WhenReadingValue(byte value, byte expectedResult)
    {
        var ramController = new RamControllerSpy();
        var bus = CreatePpuBus();
        var address = new OamAddressPort(ramController, bus);
        var mask = new Mask(ramController, bus);
        var sut = new OamDataPort(address, mask, new RasterAddress(), CreatePpuBus());
        sut.Write(value);
        Assert.Equal(expectedResult, sut.Read());
    }

    private static IPpuIOBus CreatePpuBus() => new PpuIOBus(new Clock(0));

    [Fact]
    public void AdvanceAddress_WhenWritingDataToDataPort()
    {
        var ram = new byte[0x3000];
        ram[0x2003] = 0x12;

        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var bus = CreatePpuBus();
        var address = new OamAddressPort(ramController, bus);
        var mask = new Mask(ramController, bus);
        var sut = new OamDataPort(address, mask, new RasterAddress(), CreatePpuBus());
        sut.Write(0xff);
        Assert.Equal(0x13, ram[0x2003]);
    }

    [Fact]
    public void DoNotAdvanceAddress_WhenReadingDataToDataPort()
    {
        var ram = new byte[0x3000];
        ram[0x2003] = 0x12;

        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var bus = CreatePpuBus();
        var address = new OamAddressPort(ramController, CreatePpuBus());
        var mask = new Mask(ramController, bus);
        var sut = new OamDataPort(address, mask, new RasterAddress(), CreatePpuBus());
        _ = sut.Read();
        Assert.Equal(0x12, ram[0x2003]);
    }
}