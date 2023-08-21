namespace NesCs.Logic.Ppu;

public class ByteToggle : IByteToggle
{
    private int _toggle;

    public int GetIndex()
    {
        var result = _toggle;
        _toggle = (_toggle + 1) & 1;
        return result;
    }

    public void Reset() => _toggle = 0;
}