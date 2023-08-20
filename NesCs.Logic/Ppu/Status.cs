using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Status
{
    private const int StatusIndex = 0x2002;
    private readonly IRamController _ramController;

    private byte Flags
    {
        get => _ramController.DirectReadFrom(StatusIndex);
        set => _ramController.DirectWriteTo(StatusIndex, value);
    }

    public Status(IRamController ramController) => _ramController = ramController;

    public byte OpenBus
    {
        get => (byte)(Flags & 0b11111);
        set => Flags |= (byte)(value & 0b11111);
    }

    public byte O
    {
        get => (byte)((Flags >> 5) & 1);
        set => Flags |= (byte)((value & 1) << 5);
    }

    public byte S
    {
        get => (byte)((Flags >> 6) & 1);
        set => Flags |= (byte)((value & 1) << 6);
    }

    public byte V
    {
        get => (byte)((Flags >> 7) & 1);
        set => Flags |= (byte)((value & 1) << 7);
    }

    public void Write(byte value) => Flags = value;

    public byte Read() => Flags;
}