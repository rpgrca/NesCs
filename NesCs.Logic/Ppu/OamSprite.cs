namespace NesCs.Logic.Ppu;

public class OamSprite
{
    private readonly byte[] _sprite;
    public OamSpriteAttribute Attributes { get; private set; }

    public OamSprite(ref byte[] sprite)
    {
        _sprite = sprite;
        Attributes = new OamSpriteAttribute(ref _sprite);
    }

    public byte PositionY
    {
        get => _sprite[0];
        set => _sprite[0] = value;
    }

    public byte IndexNumber
    {
        get => _sprite[1];
        set => _sprite[1] = value;
    }

    public byte PositionX
    {
        get => _sprite[3];
        set => _sprite[3] = value;
    }
}