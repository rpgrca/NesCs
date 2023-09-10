namespace NesCs.Logic.Ppu;

public class OamDataPort
{
    private readonly OamAddressPort _address;
    private readonly Mask _mask;
    private readonly RasterAddress _raster;
    private readonly IPpuIOBus _ioBus;
    private readonly OamSprite[] _data;

    public OamDataPort(OamAddressPort address, Mask mask, RasterAddress raster, IPpuIOBus ioBus)
    {
        _address = address;
        _mask = mask;
        _raster = raster;
        _ioBus = ioBus;

        _data = new OamSprite[64];
        for (var index = 0; index < 64; index++)
        {
            _data[index] = new OamSprite();
        }
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
        var index = Math.DivRem(_address.Read() , 4, out var remainder);
        _data[index].Write(remainder, value);
        _address.IncrementAddress();
    }

    public byte Read()
    {
        return _ioBus.Read();
        /*
        var index = Math.DivRem(_pointer, 4, out var remainder);
        var result = _data[index].Read(remainder);
        _ioBus.Write(result);
        return result;*/
    }
}