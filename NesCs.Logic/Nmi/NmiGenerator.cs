using NesCs.Logic.Clocking;
using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;

namespace NesCs.Logic.Nmi;

public class NmiGenerator : INmiGenerator
{
    private byte _controlSet;
    private byte _statusSet;
    private Cpu6502? _cpu;
    private readonly IClock _clock;
    private readonly RasterAddress _rasterAddress;

    public NmiGenerator(IClock clock, RasterAddress rasterAddress)
    {
        _clock = clock;
        _rasterAddress = rasterAddress;
    }

    public void SetControl(byte value)
    {
        if (_controlSet != value)
        {
            _controlSet = value;
            CheckForNmi();
        }
    }

    public void SetStatus(byte value)
    {
        if (_statusSet != value)
        {
            _statusSet = value;
            if (value == 1)
            {
                CheckForNmi();
            }
        }
    }

    private void CheckForNmi()
    {
        if (_controlSet + _statusSet == 2)
        {
            if (!_rasterAddress.IgnoringVblank)
            {
                _cpu?.SetNmiFlipFlop();
            }
        }
    }

    public void AttachTo(Cpu6502 cpu) => _cpu = cpu;

    public void IgnoreVblankThisFrame() => _rasterAddress.IgnoreVblankThisFrame();
}