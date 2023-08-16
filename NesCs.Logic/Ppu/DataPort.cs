namespace NesCs.Logic.Ppu;

public class DataPort
{
    private byte _data;

    public byte Data
    {
        get => _data;
        set
        {
            _data = value;
        }
    }

    public void Write(byte value) => Data = value;
}