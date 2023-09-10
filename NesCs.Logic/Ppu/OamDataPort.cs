namespace NesCs.Logic.Ppu;

public class OamDataPort
{
    private readonly OamAddressPort _address;
    private readonly Mask _mask;
    private readonly RasterAddress _raster;
    private readonly IPpuIOBus _ioBus;
    private byte[] _data;

    public OamDataPort(OamAddressPort address, Mask mask, RasterAddress raster, IPpuIOBus ioBus)
    {
        _address = address;
        _mask = mask;
        _raster = raster;
        _ioBus = ioBus;
        _data = new byte[256];
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
        _data[_address.Address] = value;
        _address.IncrementAddress();
    }

    public byte Read()
    {
        var result = (byte)(_data[_address.Address] & 0b11100011);
        _ioBus.Write(result);
        return (byte)(result & 0b11100011);
    }
}