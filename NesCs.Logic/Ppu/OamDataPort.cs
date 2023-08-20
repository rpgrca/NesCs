namespace NesCs.Logic.Ppu;

public class OamDataPort
{
    private readonly OamAddressPort _address;
    private byte _data;

    public OamDataPort(OamAddressPort address) =>
        _address = address;

    public void Write(byte value)
    {
        _data = value;
        _address.IncrementAddress();
    }

    public byte Read() => _data;
}