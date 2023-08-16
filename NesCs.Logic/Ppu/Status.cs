namespace NesCs.Logic.Ppu;

public class Status
{
    private byte _flags;

    public byte OpenBus
    {
        get => (byte)(_flags & 0b11111);
        set => _flags |= (byte)(value & 0b11111);
    }

    public byte O
    {
        get => (byte)((_flags >> 5) & 1);
        set => _flags |= (byte)((value & 1) << 5);
    }

    public byte S
    {
        get => (byte)((_flags >> 6) & 1);
        set => _flags |= (byte)((value & 1) << 6);
    }

    public byte V
    {
        get => (byte)((_flags >> 7) & 1);
        set => _flags |= (byte)((value & 1) << 7);
    }

    public void Write(byte value) => _flags = value;
}