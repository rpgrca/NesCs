using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Mask
{
    private const int MaskIndex = 0x2001;

    private readonly IRamController _ramController;
    private readonly IPpuIOBus _ioBus;

    public Mask(IRamController ramController, IPpuIOBus ioBus)
    {
        _ramController = ramController;
        _ioBus = ioBus;
    }

    private byte Flags
    {
        get => _ramController.DirectReadFrom(MaskIndex);
        set => _ramController.DirectWriteTo(MaskIndex, value);
    }

    public byte Grey
    {
        get => (byte)(Flags & 1);
        set => Flags |= (byte)(value & 1);
    }

    public byte Lm
    {
        get => (byte)((Flags >> 1) & 1);
        set => Flags |= (byte)((value & 1) << 1);
    }

    public byte M
    {
        get => (byte)((Flags >> 2) & 1);
        set => Flags |= (byte)((value & 1) << 2);
    }

    public byte Lb
    {
        get => (byte)((Flags >> 3) & 1);
        set => Flags |= (byte)((value & 1) << 3);
    }

    public byte Ls
    {
        get => (byte)((Flags >> 4) & 1);
        set => Flags |= (byte)((value & 1) << 4);
    }

    public byte R
    {
        get => (byte)((Flags >> 5) & 1);
        set => Flags |= (byte)((value & 1) << 5);
    }

    public byte G
    {
        get => (byte)((Flags >> 6) & 1);
        set => Flags |= (byte)((value & 1) << 6);
    }

    public byte B
    {
        get => (byte)((Flags >> 7) & 1);
        set => Flags |= (byte)((value & 1) << 7);
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
        Flags = value;
    }

    public byte Read() => _ioBus.Read();
}