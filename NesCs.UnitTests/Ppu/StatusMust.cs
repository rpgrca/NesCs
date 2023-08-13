namespace NesCs.UnitTests.Ppu;

public class StatusMust
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(0b11111100, 0b11100)]
    [InlineData(0b11110000, 0b10000)]
    public void SetOpenBusCorrectly(byte value, byte expectedValue)
    {
        var sut = new Logic.Ppu.Status
        {
            OpenBus = value
        };
        Assert.Equal(expectedValue, sut.OpenBus);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpriteOverflowCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Status
        {
            O = value
        };
        Assert.Equal(value, sut.O);
    }
}