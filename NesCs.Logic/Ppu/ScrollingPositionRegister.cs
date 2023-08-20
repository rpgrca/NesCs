using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class ScrollingPositionRegister
{
    private const int ScrollIndex = 0x2005;
    private readonly byte[] _cameraPosition = { 0, 0 };
    private readonly IRamController _ram;
    private byte _index = 0;

    public byte CameraPositionX => _cameraPosition[0];

    public byte CameraPositionY => _cameraPosition[1];

    private byte Address
    {
        get => _ram.DirectReadFrom(ScrollIndex);
        set => _ram.DirectWriteTo(ScrollIndex, value);
    }

    public ScrollingPositionRegister(IRamController ram) => _ram = ram;

    public void Write(byte value)
    {
        _cameraPosition[_index] = value;
        _index = (byte)((_index + 1) & 1);
        Address = value;
    }

    public byte Read() => Address;
}