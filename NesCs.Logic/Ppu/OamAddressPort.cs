using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class OamAddressPort
{
    public const int OamAddressIndex = 0x2003;
    private readonly IRamController _ram;
    private readonly IPpuIOBus _ioBus;

    private byte Address
    {
        get => _ram.DirectReadFrom(OamAddressIndex);
        set => _ram.DirectWriteTo(OamAddressIndex, value);
    }

    public OamAddressPort(IRamController ram, IPpuIOBus ioBus)
    {
        _ram = ram;
        _ioBus = ioBus;
    }

    internal void IncrementAddress() => Address++;

    public void Write(byte value)
    {
        _ioBus.Write(value);
        Address = value;
    }

    public byte Read() => _ioBus.Read();
}