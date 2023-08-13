namespace NesCs.UnitTests.Ppu;

public class ScrollingPositionRegisterMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetPositionCorrectly(byte value)
    {
        var sut = new Logic.Ppu.ScrollingPositionRegister
        {
            Position = value
        };

        Assert.Equal(value, sut.Position);
    }
}