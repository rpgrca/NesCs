namespace NesCs.Logic.Ppu;

public class PpuIOBus : IPpuIOBus
{
    private byte _bus;

    public byte Read() => _bus;

    public void Write(byte value) => _bus = value;
}
