namespace NesCs.UnitTests.Ppu;

public class ScrollingPositionRegisterMust
{
    [Fact]
    public void InitializePositionCorrectly()
    {
        var ramController = new RamControllerSpy();
        var sut = new Logic.Ppu.ScrollingPositionRegister(ramController);
        Assert.Equal(0, sut.CameraPositionX);
        Assert.Equal(0, sut.CameraPositionY);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetPositionCorrectly(byte value)
    {
        var ramController = new RamControllerSpy();
        var sut = new Logic.Ppu.ScrollingPositionRegister(ramController);
        sut.Write(value);
        Assert.Equal(value, sut.CameraPositionX);
        Assert.Equal(0, sut.CameraPositionY);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0xff)]
    public void SetPositionYCorrectly(byte value)
    {
        var ramController = new RamControllerSpy();
        var sut = new Logic.Ppu.ScrollingPositionRegister(ramController);
        sut.Write(0x4);
        sut.Write(value);
        Assert.Equal(4, sut.CameraPositionX);
        Assert.Equal(value, sut.CameraPositionY);
    }
}