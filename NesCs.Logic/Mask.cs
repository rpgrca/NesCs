namespace NesCs.Logic.Ppu;

public class Mask
{
    private byte _flags;

    public byte Grey
    {
        get => (byte)(_flags & 1);
        set => _flags |= (byte)(value & 1);
    }

    public byte Lm
    {
        get => (byte)((_flags >> 1) & 1);
        set => _flags |= (byte)((value & 1) << 1);
    }

    public byte M
    {
        get => (byte)((_flags >> 2) & 1);
        set => _flags |= (byte)((value & 1) << 2);
    }

    public byte Lb
    {
        get => (byte)((_flags >> 3) & 1);
        set => _flags |= (byte)((value & 1) << 3);
    }

    public byte Ls
    {
        get => (byte)((_flags >> 4) & 1);
        set => _flags |= (byte)((value & 1) << 4);
    }

    public byte R
    {
        get => (byte)((_flags >> 5) & 1);
        set => _flags |= (byte)((value & 1) << 5);
    }

    public byte G
    {
        get => (byte)((_flags >> 6) & 1);
        set => _flags |= (byte)((value & 1) << 6);
    }

    public byte B
    {
        get => (byte)((_flags >> 7) & 1);
        set => _flags |= (byte)((value & 1) << 7);
    }
}