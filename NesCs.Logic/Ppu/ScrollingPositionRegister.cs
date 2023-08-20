using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class ScrollingPositionRegister
{
    private const int ScrollIndex = 0x2005;
    private readonly byte[] _cameraPosition = { 0, 0 };
    private readonly IRamController _ram;
    private readonly IByteToggle _toggle;

    public byte CameraPositionX => _cameraPosition[0];

    public byte CameraPositionY => _cameraPosition[1];

    private byte Address
    {
        get => _ram.DirectReadFrom(ScrollIndex);
        set => _ram.DirectWriteTo(ScrollIndex, value);
    }

    public ScrollingPositionRegister(IRamController ram, IByteToggle toggle)
    {
        _ram = ram;
        _toggle = toggle;
    }

    public void Write(byte value)
    {
        _cameraPosition[_toggle.GetIndex()] = value;
        Address = value;
    }

    public byte Read() => Address;
}