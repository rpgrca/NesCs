namespace NesCs.Logic.Ppu;

public class OamDataPort
{
    private readonly OamAddressPort _address;
    private byte _data;

    public OamDataPort(OamAddressPort address) =>
        _address = address;

    public byte Data
    {
        get => _data;
        set
        {
            _data = value;
            _address.IncrementAddress();
        }
    }
}