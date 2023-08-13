namespace NesCs.Logic.Ppu;

public class Status
{
    private byte _flags;

    public byte OpenBus
    {
        get => (byte)(_flags & 0b11111);
        set => _flags |= (byte)(value & 0b11111);
    }
}
