using NesCs.Logic.Cpu;

namespace NesCs.Logic.Ppu;

public class PpuIOBus : IPpuIOBus
{
    private readonly IClock _clock;
    private byte _bus;
    private int _cycle = 0;

    public PpuIOBus(IClock clock) => _clock = clock;

    public byte Read()
    {
        if (_clock.GetCycles() - _cycle > 1000)
        {
            _bus = 0;
        }

        return _bus;
    }

    public void Write(byte value)
    {
        _cycle = _clock.GetCycles();
        _bus = value;
    }
}