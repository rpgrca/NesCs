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
            Grey = value
        };

        Assert.Equal(value, sut.Grey);
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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetShowSpritesFlagCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            Ls = value
        };

        Assert.Equal(value, sut.Ls);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetEmphasizeRedFlagCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            R = value
        };

        Assert.Equal(value, sut.R);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void SetEmphasizeGreenFlagCorrectly(byte value)
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
    public void SetEmphasizeBlueFlagCorrectly(byte value)
    {
        var sut = new Logic.Ppu.Mask
        {
            B = value
        };

        Assert.Equal(value, sut.B);
    }
}