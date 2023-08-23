namespace NesCs.Logic.Ppu;

public class OamDataPort
{
    private readonly OamAddressPort _address;
    private readonly IPpuIOBus _ioBus;
    private byte _data;

    public OamDataPort(OamAddressPort address, IPpuIOBus ioBus)
    {
        _address = address;
        _ioBus = ioBus;
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
        _data = value;
        _address.IncrementAddress();
    }

    public byte Read() => (byte)(_data & 0b11100011);
}