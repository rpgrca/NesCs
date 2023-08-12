namespace NesCs.UnitTests.Ppu;

public class MaskMust
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetGrayscaleCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            G = value
        };

        Assert.Equal(value, sut.G);
    }
}