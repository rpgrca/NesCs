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

    public int GetBaseNametableAddress() => 0x2000 + N * 0x400;

    public int GetSpritePatternTableAddress() => 0x1000 * S;
}