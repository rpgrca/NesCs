using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class OamAddressPort
{
    private const int OamAddressIndex = 0x2003;
    private readonly IRamController _ram;

    private byte Address
    {
        get => _ram.DirectReadFrom(OamAddressIndex);
        set => _ram.DirectWriteTo(OamAddressIndex, value);
    }

    public OamAddressPort(IRamController ram) => _ram = ram;

    internal void IncrementAddress() => Address++;

    public void Write(byte value)
    {
        Address = value;
    }

    public byte Read() => Address;
}