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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetBackgroundInLeftmostPixelsCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            Lm = value
        };

        Assert.Equal(value, sut.Lm);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetSpritesFlagCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            M = value
        };

        Assert.Equal(value, sut.M);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetShowBackgroundFlagCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            Lb = value
        };

        Assert.Equal(value, sut.Lb);
    }

}