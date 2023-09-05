namespace NesCs.Logic.Ppu;

public class OamSprite
{
    private readonly byte[] _sprite = { 0, 0, 0, 0 };
    public OamSpriteAttribute Attributes { get; private set; }

    public OamSprite() => Attributes = new OamSpriteAttribute(ref _sprite);

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

    internal void Clear()
    {
        _sprite[0] = _sprite[1] = _sprite[2] = _sprite[3] = 0xFF;
    }
}