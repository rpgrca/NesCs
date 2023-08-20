namespace NesCs.Logic.Ppu;

public class DataPort
{
    private readonly IPpuVram _ppuVram;
    private byte _cache;

    public DataPort(IPpuVram ppuVram) => _ppuVram = ppuVram;

    public void Write(byte value)
    {
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
            result = _cache = _ppuVram.Read();
        }

        _ppuVram.IncrementAddress();
        return result;
    }
}