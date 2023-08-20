namespace NesCs.Logic.Ppu;

public class DataPort
{
    private readonly IPpuVram _ppuVram;

    public DataPort(IPpuVram ppuVram) => _ppuVram = ppuVram;

    public void Write(byte value)
    {
        _ppuVram.Write(value);
        _ppuVram.IncrementAddress();
    }

    public byte Read()
    {
        var value = _ppuVram.Read();
        _ppuVram.IncrementAddress();
        return value;
    }
}