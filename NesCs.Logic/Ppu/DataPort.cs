namespace NesCs.Logic.Ppu;

public class DataPort
{
    private readonly IPpuVram _ppuVram;
    private readonly IPpuIOBus _ioBus;
    private byte _cache;

    public DataPort(IPpuVram ppuVram, IPpuIOBus ioBus)
    {
        _ppuVram = ppuVram;
        _ioBus = ioBus;
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
        _ppuVram.Write(value);
        _ppuVram.IncrementAddress();
    }

    public byte Read()
    {
        var result = _cache;
        if (_ppuVram.CurrentAddress <= 0x3EFF)
        {
            _cache = _ppuVram.Read();
            _ioBus.Write(_cache);
        }
        else
        {
            result = _cache = _ppuVram.Read();
            _ioBus.Write(result);
        }

        _ppuVram.IncrementAddress();
        return result;
    }
}