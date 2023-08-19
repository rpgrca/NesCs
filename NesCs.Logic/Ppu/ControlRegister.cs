using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class ControlRegister
{
    private const int ControlRegisterIndex = 0x2000;
    private readonly IRamController _ram;

    public ControlRegister(IRamController ram) => _ram = ram;

    private byte Flag
    {
        get => _ram[ControlRegisterIndex];
        set => _ram[ControlRegisterIndex] = value;
    }

    public byte N
    {
        get => (byte)(Flag & 0b11);
        set => Flag |= (byte)(value & 0b11);
    }

    public byte I
    {
        get => (byte)((Flag & 0b100) >> 2);
        set => Flag |= (byte)((value & 1) << 2);
    }

    public byte S
    {
        get => (byte)((Flag & 0b1000) >> 3);
        set => Flag |= (byte)((value & 1) << 3);
    }

    public byte B
    {
        get => (byte)((Flag & 0b10000) >> 4);
        set => Flag |= (byte)((value & 1) << 4);
    }

    public byte H
    {
        get => (byte)((Flag & 0b100000) >> 5);
        set => Flag |= (byte)((value & 1) << 5);
    }

    public byte P
    {
        get => (byte)((Flag & 0b1000000) >> 6);
        set => Flag |= (byte)((value & 1) << 6);
    }

    public byte V
    {
        get => (byte)((Flag & 0b10000000) >> 7);
        set => Flag |= (byte)((value & 1) << 7);
    }

    public void Write(byte value, byte[] ram, IPpu ppu) => ram[ControlRegisterIndex] = value;

    public int GetBaseNametableAddress() => 0x2000 + N * 0x400;

    public int GetSpritePatternTableAddress() => 0x1000 * S;

    public int GetBackgroundPatternTableAddress() => 0x1000 * B;
}