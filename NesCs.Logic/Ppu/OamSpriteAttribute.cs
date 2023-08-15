namespace NesCs.Logic.Ppu;

public class OamSpriteAttribute
{
    private byte[] _flags;

    public OamSpriteAttribute(ref byte[] flags) => _flags = flags;

    public byte Palette
    {
        get => (byte)(_flags[2] & 0b11);
        set => _flags[2] |= (byte)(value & 0b11);
    }
}