namespace NesCs.Logic.Ppu;

public class ScrollingPositionRegister
{
    private const int ScrollIndex = 0x2005;
    private byte _index = 0;
    private byte[] _cameraPosition = { 0, 0 };

    public byte CameraPositionX => _cameraPosition[0];

    public byte CameraPositionY => _cameraPosition[1];

    public byte Position {
        private get => 0;
        set
        {
            _cameraPosition[_index] = value;
            _index = (byte)((_index + 1) & 1);
        }
    }

    public void Write(byte value, byte[] ram, IPpu ppu)
    {
        Position = value;
        ram[ScrollIndex] = value;
    }
}