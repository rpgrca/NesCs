namespace NesCs.UnitTests.Ppu;

public class ControlRegisterMust
{
    [Theory]
    [InlineData(0, 0x2000)]
    [InlineData(1, 0x2400)]
    [InlineData(2, 0x2800)]
    [InlineData(3, 0x2C00)]
    public void CalculateBaseNametableAddressCorrectly(byte value, int expectedAddress)
    {
        var sut = new Logic.Ppu.ControlRegister();
        sut.SetBaseNametableAddress(value);
        Assert.Equal(expectedAddress, sut.GetBaseNametableAddress());
    }
}