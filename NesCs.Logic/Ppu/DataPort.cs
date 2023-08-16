namespace NesCs.Logic.Ppu;

public class DataPort
{
    private const int DataPortIndex = 0x2007;
    private byte _data;

    public byte Data
    {
        get => _data;
        set
        {
            _data = value;
        }
    }

    public void Write(byte value, byte[] ram)
    {
        ram[DataPortIndex] = value;
        Data = value;
    }
}