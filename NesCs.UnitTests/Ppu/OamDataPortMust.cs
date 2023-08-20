using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class OamDataPortMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetDataCorrectly(byte value)
    {
        var ramController = new RamControllerSpy();
        var address = new Logic.Ppu.OamAddressPort(ramController);
        var sut = new Logic.Ppu.OamDataPort(address);
        sut.Write(value);
        Assert.Equal(value, sut.Read());
    }

    [Fact]
    public void AdvanceAddress_WhenWritingDataToDataPort()
    {
        var ram = new byte[0x3000];
        ram[0x2003] = 0x12;

        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var address = new Logic.Ppu.OamAddressPort(ramController);
        var sut = new Logic.Ppu.OamDataPort(address);
        sut.Write(0xff);
        Assert.Equal(0x13, ram[0x2003]);
    }

    [Fact]
    public void DoNotAdvanceAddress_WhenReadingDataToDataPort()
    {
        var ram = new byte[0x3000];
        ram[0x2003] = 0x12;

        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var address = new Logic.Ppu.OamAddressPort(ramController);
        var sut = new Logic.Ppu.OamDataPort(address);
        _ = sut.Read();
        Assert.Equal(0x12, ram[0x2003]);
    }
}