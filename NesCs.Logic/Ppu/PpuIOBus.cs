using NesCs.Logic.Clocking;

namespace NesCs.Logic.Ppu;

// TODO: Implement IClockHook?
public class PpuIOBus : IPpuIOBus
{
    private readonly IClock _clock;
    private byte _bus;
    private int _cycle = 0;

    public PpuIOBus(IClock clock) => _clock = clock;

    public void Clear() => _bus = 0;

    public byte Read()
    {
        if (_bus != 0 && _clock.GetCycles() - _cycle > 1_500_000)
        {
            _bus = 0;
        }

        return _bus;
    }

    public void Refresh() => _cycle = _clock.GetCycles();

    public void Write(byte value)
    {
        _cycle = _clock.GetCycles();
        _bus = value;
    }
}