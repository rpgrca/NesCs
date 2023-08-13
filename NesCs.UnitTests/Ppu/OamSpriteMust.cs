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
}