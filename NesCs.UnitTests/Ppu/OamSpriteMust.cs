using NesCs.Logic.Ppu;

namespace NesCs.UnitTests.Ppu;

public class OamSpriteMust
{
    [Fact]
    public void BeInitializedCorrectly()
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array);
        Assert.Equal(0, sut.PositionY);
        Assert.Equal(0, sut.IndexNumber);
        Assert.Equal(0, sut.Attributes.Palette);
        Assert.Equal(0, sut.PositionX);
    }

    [Fact]
    public void SetSpritePositionYCorrectly()
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array)
        {
            PositionY = 0x17
        };

        Assert.Equal(0x17, sut.PositionY);
        Assert.Equal(0x17, array[0]);
    }

    [Fact]
    public void SetIndexNumberCorrectly()
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array)
        {
            IndexNumber = 0x18
        };

        Assert.Equal(0x18, sut.IndexNumber);
        Assert.Equal(0x18, array[1]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void SetPaletteCorrectly(byte value)
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array);
        sut.Attributes.Palette = value;
        Assert.Equal(value, sut.Attributes.Palette);
        Assert.Equal(value, array[2]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0b100000)]
    public void SetPriorityCorrectly(byte value, byte expectedFlag)
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array);
        sut.Attributes.Priority = value;
        Assert.Equal(value, sut.Attributes.Priority);
        Assert.Equal(expectedFlag, array[2]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0b1000000)]
    public void SetFlipHorizontallyCorrectly(byte value, byte expectedFlag)
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array);
        sut.Attributes.FlipHorizontally = value;
        Assert.Equal(value, sut.Attributes.FlipHorizontally);
        Assert.Equal(expectedFlag, array[2]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0b10000000)]
    public void SetFlipVerticallyCorrectly(byte value, byte expectedFlag)
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array);
        sut.Attributes.FlipVertically = value;
        Assert.Equal(value, sut.Attributes.FlipVertically);
        Assert.Equal(expectedFlag, array[2]);
    }

    [Fact]
    public void SetSpritePositionXCorrectly()
    {
        byte[] array = { 0, 0, 0, 0 };
        var sut = new OamSprite(ref array)
        {
            PositionX = 0x17
        };

        Assert.Equal(0x17, sut.PositionX);
        Assert.Equal(0x17, array[3]);
    }
}