using NesCs.Logic.Nmi;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class ControlRegister
{
    private const int ControlRegisterIndex = 0x2000;
    private readonly IRamController _ram;
    private readonly IPpuIOBus _ioBus;
    private readonly INmiGenerator _nmiGenerator;

    public ControlRegister(IRamController ram, IPpuIOBus ioBus, INmiGenerator nmiGenerator)
    {
        _ram = ram;
        _ioBus = ioBus;
        _nmiGenerator = nmiGenerator;
    }

    private byte Flag
    {
        get => _ram.DirectReadFrom(ControlRegisterIndex);
        set => _ram.DirectWriteTo(ControlRegisterIndex, value);
    }

    public byte N
    {
        get => (byte)(Flag & 0b11);
        set => Flag |= (byte)(value & 0b11); // TODO: Must clear before setting
    }

    public byte I
    {
        get => (byte)((Flag & 0b100) >> 2);
        set => Flag |= (byte)((value & 1) << 2); // TODO: Must clear before setting
    }

    public byte S
    {
        get => (byte)((Flag & 0b1000) >> 3);
        set => Flag |= (byte)((value & 1) << 3); // TODO: Must clear before setting
    }

    public byte B
    {
        get => (byte)((Flag & 0b10000) >> 4);
        set => Flag |= (byte)((value & 1) << 4); // TODO: Must clear before setting
    }

    public byte H
    {
        get => (byte)((Flag & 0b100000) >> 5);
        set => Flag |= (byte)((value & 1) << 5); // TODO: Must clear before setting
    }

    public byte P
    {
        get => (byte)((Flag & 0b1000000) >> 6);
        set => Flag |= (byte)((value & 1) << 6); // TODO: Must clear before setting
    }

    public byte V
    {
        get => (byte)((Flag & 0b10000000) >> 7);
        set
        {
            Flag = (byte)((Flag & ~0b10000000) | (value & 1) << 7);
            _nmiGenerator.SetControl(value);
        }
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
        Flag = value;
        // TODO: should not duplicate logic from V
        if (V == 1)
        {
            _nmiGenerator.SetControl(1);
        }
        else
        {
            _nmiGenerator.SetControl(0);
        }
    }

    public byte Read() => _ioBus.Read();

    public int GetBaseNametableAddress() => 0x2000 + N * 0x400;

    public int GetSpritePatternTableAddress() => 0x1000 * S;

    public int GetBackgroundPatternTableAddress() => 0x1000 * B;
}