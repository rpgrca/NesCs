namespace NesCs.Logic.Ppu;

public class OamSpriteAttribute
{
    private readonly byte[] _flags;

    public OamSpriteAttribute(ref byte[] flags) => _flags = flags;

    public byte Palette
    {
        get => (byte)(_flags[2] & 0b11);
        set => _flags[2] |= (byte)(value & 0b11);
    }
    
    public byte Priority
    {
        get => (byte)((_flags[2] >> 5) & 1);
        set => _flags[2] |= (byte)((value & 1) << 5);
    }

    public byte FlipHorizontally
    {
        get => (byte)((_flags[2] >> 6) & 1);
        set => _flags[2] |= (byte)((value & 1) << 6);
    }

    public byte FlipVertically
    {
        get => (byte)((_flags[2] >> 7) & 1);
        set => _flags[2] |= (byte)((value & 1) << 7);
    }
}