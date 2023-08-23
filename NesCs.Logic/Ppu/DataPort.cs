namespace NesCs.Logic.Ppu;

public class DataPort
{
    public const int DataIndex = 0x2007;
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
        }
        else
        {
            result = _cache = (byte)((_ioBus.Read() & 0b11000000) | (_ppuVram.Read() & 0b111111));
        }

        _ppuVram.IncrementAddress();

        _ioBus.Write(result);
        return result;
    }
}