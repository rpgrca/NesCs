namespace NesCs.Logic.Ppu;

public class Mask
{
    private byte _flags;

    public byte G
    {
        get => (byte)(_flags & 1);
        set => _flags |= (byte)(value & 1);
    }
}
