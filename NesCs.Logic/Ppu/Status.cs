using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Status
{
    private const int StatusIndex = 0x2002;
    private readonly IRamController _ramController;

    private byte Flag
    {
        get => _ramController[StatusIndex];
        set => _ramController[StatusIndex] = value;
    }

    public Status(IRamController ramController) => _ramController = ramController;

    public byte OpenBus
    {
        get => (byte)(Flag & 0b11111);
        set => Flag |= (byte)(value & 0b11111);
    }

    public byte O
    {
        get => (byte)((Flag >> 5) & 1);
        set => Flag |= (byte)((value & 1) << 5);
    }

    public byte S
    {
        get => (byte)((Flag >> 6) & 1);
        set => Flag |= (byte)((value & 1) << 6);
    }

    public byte V
    {
        get => (byte)((Flag >> 7) & 1);
        set => Flag |= (byte)((value & 1) << 7);
    }

    public void Write(byte value, byte[] ram, IPpu ppu) => ram[StatusIndex] = value;
}