namespace NesCs.UnitTests.Ppu;

public class AddressRegisterMust
{
    [Fact]
    public void InitializeAddressCorrectly()
    {
        var sut = new Logic.Ppu.AddressRegister();
        Assert.Equal(0, sut.UpperByte);
        Assert.Equal(0, sut.LowerByte);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0x30)]
    public void SetAddressUpperByteCorrectly(byte value)
    {
        var sut = new Logic.Ppu.AddressRegister
        {
            Address = value
        };

        Assert.Equal(value, sut.UpperByte);
        Assert.Equal(0, sut.LowerByte);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetAddressLowerByteCorrectly(byte value)
    {
        var sut = new Logic.Ppu.AddressRegister();
        sut.Address = 4;
        sut.Address = value;
        Assert.Equal(4, sut.UpperByte);
        Assert.Equal(value, sut.LowerByte);
    }

    [Theory]
    [InlineData(0x40, 0)]
    [InlineData(0x50, 16)]
    public void MirrorDownValue_WhenAddressGoesAbove3fff(byte value, byte expectedValue)
    {
        var sut = new Logic.Ppu.AddressRegister();
        sut.Address = value;
        sut.Address = 0x00;
        Assert.Equal(expectedValue, sut.UpperByte);
        Assert.Equal(0, sut.LowerByte);
    }
}