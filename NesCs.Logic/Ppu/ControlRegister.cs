namespace NesCs.Logic.Ppu;

public class ControlRegister
{
    private byte _flags;

    public byte N
    {
        get => (byte)(_flags & 0b11);
        set => _flags |= (byte)(value & 0b11);
    }

    public byte I
    {
        get => (byte)((_flags & 0b100) >> 2);
        set => _flags |= (byte)((value & 1) << 2);
    }

    public byte S
    {
        get => (byte)((_flags & 0b1000) >> 3);
        set => _flags |= (byte)((value & 1) << 3);
    }

    public byte B
    {
        get => (byte)((_flags & 0b10000) >> 4);
        set => _flags |= (byte)((value & 1) << 4);
    }

    public byte H
    {
        get => (byte)((_flags & 0b100000) >> 5);
        set => _flags |= (byte)((value & 1) << 5);
    }

    public byte P
    {
        get => (byte)((_flags & 0b1000000) >> 6);
        set => _flags |= (byte)((value & 1) << 6);
    }

    public byte V
    {
        get => (byte)((_flags & 0b10000000) >> 7);
        set => _flags |= (byte)((value & 1) << 7);
    }

    public void Write(byte value) => _flags = value;

    public int GetBaseNametableAddress() => 0x2000 + N * 0x400;

    public int GetSpritePatternTableAddress() => 0x1000 * S;

    public int GetBackgroundPatternTableAddress() => 0x1000 * B;
}