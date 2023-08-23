using NesCs.Logic.Cpu;
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
        var address = new OamAddressPort(ramController, CreatePpuBus());
        var sut = new OamDataPort(address, CreatePpuBus());
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
        var address = new OamAddressPort(ramController, CreatePpuBus());
        var sut = new OamDataPort(address, CreatePpuBus());
        sut.Write(0xff);
        Assert.Equal(0x13, ram[0x2003]);
    }

    [Fact]
    public void DoNotAdvanceAddress_WhenReadingDataToDataPort()
    {
        var ram = new byte[0x3000];
        ram[0x2003] = 0x12;

        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var address = new OamAddressPort(ramController, CreatePpuBus());
        var sut = new OamDataPort(address, CreatePpuBus());
        _ = sut.Read();
        Assert.Equal(0x12, ram[0x2003]);
    }
}