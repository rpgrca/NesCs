namespace NesCs.UnitTests.Ppu;

public class ControlRegisterMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void SetBaseNametableAddressBitsCorrectly(byte value)
    {
        var sut = new Logic.Ppu.ControlRegister
        {
            N = value
        };

        Assert.Equal(value, sut.N);
    }

    [Theory]
    [InlineData(0, 0x2000)]
    [InlineData(1, 0x2400)]
    [InlineData(2, 0x2800)]
    [InlineData(3, 0x2C00)]
    public void CalculateBaseNametableAddressCorrectly(byte value, int expectedAddress)
    {
        var sut = new Logic.Ppu.ControlRegister
        {
            N = value
        };
        Assert.Equal(expectedAddress, sut.GetBaseNametableAddress());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetVramAddressIncrementCorrectly(byte value)
    {
        var sut = new Logic.Ppu.ControlRegister
        {
            I = value
        };
        Assert.Equal(value, sut.I);
    }
}