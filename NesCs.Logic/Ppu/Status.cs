using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Status
{
    private const int StatusIndex = 0x2002;
    private readonly IRamController _ramController;
    private readonly IByteToggle _toggle;
    private readonly IPpuIOBus _ioBus;

    private byte Flags
    {
        get
        {
            var value = _ramController.DirectReadFrom(StatusIndex);
            var openBus = _ioBus.Read();
            return (byte)((value & 0b11100000) | (openBus & 0b11111));
        }
        set => _ramController.DirectWriteTo(StatusIndex, value);
    }

    public Status(IRamController ramController, IByteToggle toggle, IPpuIOBus ioBus)
    {
        _ramController = ramController;
        _toggle = toggle;
        _ioBus = ioBus;
    }

    public byte OpenBus
    {
        get => (byte)(_ioBus.Read() & 0b11111);
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

    public void Write(byte value)
    {
        _ioBus.Write(value);
        Flags = value;
    }

    public byte Read()
    {
        _toggle.Reset();
        return Flags;
    }
}