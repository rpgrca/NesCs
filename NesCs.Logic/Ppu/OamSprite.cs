namespace NesCs.Logic.Ppu;

public class OamSprite
{
    private readonly byte[] _sprite;

    public OamSprite(ref byte[] sprite) => _sprite = sprite;

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
}