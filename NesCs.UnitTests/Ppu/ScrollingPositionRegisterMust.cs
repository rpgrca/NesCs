namespace NesCs.UnitTests.Ppu;

public class ScrollingPositionRegisterMust
{
    [Fact]
    public void InitializePositionCorrectly()
    {
        var sut = new Logic.Ppu.ScrollingPositionRegister();
        Assert.Equal(0, sut.CameraPositionX);
        Assert.Equal(0, sut.CameraPositionY);
    }

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

        Assert.Equal(value, sut.CameraPositionX);
        Assert.Equal(0, sut.CameraPositionY);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetPositionYCorrectly(byte value)
    {
        var sut = new Logic.Ppu.ScrollingPositionRegister();
        sut.Position = 4;
        sut.Position = value;
        Assert.Equal(4, sut.CameraPositionX);
        Assert.Equal(value, sut.CameraPositionY);
    }
}