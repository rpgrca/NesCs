namespace NesCs.UnitTests.Ppu;

public class OamAddressPortMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetAddressCorrectly(byte value)
    {
        var sut = new Logic.Ppu.OamAddressPort
        {
            Address = value
        };

        Assert.Equal(value, sut.Address);
    }
}