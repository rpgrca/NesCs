using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class OamAddressPortMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetAddressCorrectly(byte value)
    {
        var ram = new byte[0x3000];
        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var sut = new Logic.Ppu.OamAddressPort(ramController);
        sut.Write(value);
        Assert.Equal(value, sut.Read());
    }
}