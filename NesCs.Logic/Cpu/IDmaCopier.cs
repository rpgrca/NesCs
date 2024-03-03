using NesCs.Logic.Clocking;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

public interface IDmaCopier
{
    void AddCpu(Cpu6502 cpu);
    void Execute(byte value);
}

public class DmaCopier : IDmaCopier, IClockHook
{
    private readonly IRamController _ramController;
    private readonly IPpu _ppu;
    private Cpu6502? _cpu;
    private bool _executing;
    private byte _high;
    private int _low;
    private bool _read;
    private byte _currentValue;

    public DmaCopier(IRamController ramController, IPpu ppu)
    {
        _ramController = ramController;
        _ppu = ppu;
    }

    public int MasterClockDivisor => throw new NotImplementedException();

    public void AddCpu(Cpu6502 cpu)
    {
        _cpu = cpu;
    }

    public void Execute(byte value)
    {
        _executing = true;
        _high = value;
        _low = 0;
        _read = true;
        _cpu?.Suspend();
    }

    private int GetAddress() => _high << 8 | _low;

    public string GetStatus()
    {
        return "";
    }

    public bool Trigger(IClock clock)
    {
        if (_executing)
        {
            if (_read)
            {
                _currentValue = _ramController.DirectReadFrom(GetAddress());
                _read = false;
            }
            else
            {
                _read = true;
                //_ppu.OamDma.Write(_currentValue);
            }
        }
        else
        {
            _cpu?.Unsuspend();
        }

        return true;
    }
}