using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Status
{
    private const int StatusIndex = 0x2002;
    private readonly IRamController _ramController;
    private readonly IByteToggle _toggle;

    private byte Flags
    {
        get => _ramController.DirectReadFrom(StatusIndex);
        set => _ramController.DirectWriteTo(StatusIndex, value);
    }

    public Status(IRamController ramController, IByteToggle toggle)
    {
        _ramController = ramController;
        _toggle = toggle;
    }

    public byte OpenBus
    {
        get => (byte)(Flags & 0b11111);
        set => Flags |= (byte)(value & 0b11111);
    }

    public byte O
    {
        get
        {
            _toggle.Reset();
            return (byte)((Flags >> 5) & 1);
        }

        set => Flags |= (byte)((value & 1) << 5);
    }

    public byte S
    {
        get
        {
            _toggle.Reset();
            return (byte)((Flags >> 6) & 1);
        }

        set => Flags |= (byte)((value & 1) << 6);
    }

    public byte V
    {
        get
        {
            _toggle.Reset();
            return (byte)((Flags >> 7) & 1);
        }

        set => Flags |= (byte)((value & 1) << 7);
    }

    public void Write(byte value) => Flags = value;

    public byte Read()
    {
        _toggle.Reset();
        return Flags;
    }
}