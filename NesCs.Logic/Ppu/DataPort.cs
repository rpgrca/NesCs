namespace NesCs.Logic.Ppu;

public class DataPort
{
    private readonly IPpuVram _ppuVram;
    private readonly IPpuIOBus _ioBus;

    public DataPort(IPpuVram ppuVram, IPpuIOBus ioBus)
    {
        _ppuVram = ppuVram;
        _ioBus = ioBus;
    }

    public void Write(byte value)
    {
        _ppuVram.Write(value);
        _ppuVram.IncrementAddress();
    }

    public byte Read()
    {
        var result = _ioBus.Read();
        if (_ppuVram.CurrentAddress <= 0x3EFF)
        {
            _ioBus.Write(_ppuVram.Read());
        }
        else
        {
            result = _ppuVram.Read();
            _ioBus.Write(result);
        }

        _ppuVram.IncrementAddress();
        return result;
    }
}