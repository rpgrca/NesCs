using System.Diagnostics;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

[DebuggerDisplay("{_cycles}")]
public class Clock : IClock
{
    private int _ticks;
    private bool _stopped;
    private IClockHook[] _callbacks;

    public Clock(int ticks)
    {
        _ticks = ticks;
        _stopped = false;
        _callbacks = new IClockHook[2] { null, null };
    }

    public int GetCycles() => _ticks;

    private bool HangUp() => _ticks > 30_000_000;

    private void Tick()
    {
        var cpuText = _callbacks[0].GetStatus();
        var ppuText = _callbacks[1].GetStatus();

        var canRefresh = _callbacks[0].Trigger(_ticks);
        _callbacks[1].Trigger(_ticks);

        if (canRefresh)
        {
            Debug.Print($"{cpuText} {ppuText}");
        }

        _ticks++;
    }

    public void AddCpu(IClockHook cpu) => _callbacks[0] = cpu;

    public void AddPpu(IClockHook ppu) => _callbacks[1] = ppu;

    public void Run()
    {
        try
        {
            while (! _stopped)
            {
                Tick();

                if (HangUp())
                {
                    _stopped = true;
                }
            }
        }
        catch (Exception ex)
        {
            var error = $"{ex.Message} (on tick {_ticks})";
            Console.WriteLine(error);
            Debug.Print(error);
            throw;
        }
    }
}