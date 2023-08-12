namespace NesCs.Logic.Ppu;

public class ControlRegister
{
    private byte _flags;

    public void SetBaseNametableAddress(byte value) =>
        _flags |= (byte)(value & 0b11);

    public int GetBaseNametableAddress() =>
        0x2000 + (_flags & 0b11) * 0x400;
}