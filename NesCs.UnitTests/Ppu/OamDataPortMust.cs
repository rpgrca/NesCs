namespace NesCs.UnitTests.Ppu;

public class OamDataPortMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetDataCorrectly(byte value)
    {
        var address = new Logic.Ppu.OamAddressPort();
        var sut = new Logic.Ppu.OamDataPort(address)
        {
            Data = value
        };
        Assert.Equal(value, sut.Data);
    }

    [Fact]
    public void AdvanceAddress_WhenWritingDataToDataPort()
    {
        var address = new Logic.Ppu.OamAddressPort
        {
            Address = 0x12
        };
        var sut = new Logic.Ppu.OamDataPort(address);
        sut.Data = 0xff;
        Assert.Equal(0x13, address.Address);
    }

    [Fact]
    public void DoNotAdvanceAddress_WhenReadingDataToDataPort()
    {
        var address = new Logic.Ppu.OamAddressPort
        {
            Address = 0x12
        };
        var sut = new Logic.Ppu.OamDataPort(address);
        _ = sut.Data;
        Assert.Equal(0x12, address.Address);
    }
}