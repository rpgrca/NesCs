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

    public int GetBaseNametableAddress() => 0x2000 + (_flags & 0b11) * 0x400;
}