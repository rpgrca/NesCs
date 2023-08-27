using System.Diagnostics;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

[DebuggerDisplay("{_ticks}")]
public class Clock : IClock
{
    private int _ticks;
    private IClockHook[] _callbacks;

    public bool Aborted { get; private set; }

    public Clock(int ticks)
    {
        _ticks = ticks;
        _callbacks = new IClockHook[2] { null, null };
        Aborted = false;
    }

    public int GetCycles() => _ticks;

    private bool HangUp()
    {
        if (_ticks > 100_000_000)
        {
            return true;
        }

        return false;
         //(_ticks > 30_000_000);
    }

    private void Tick()
    {
        var cpuText = _callbacks[0].GetStatus();
        var ppuText = _callbacks[1]?.GetStatus();

        var canRefresh = _callbacks[0].Trigger(this);
        _callbacks[1]?.Trigger(this);

        /*if (canRefresh)
        {
            Debug.Print($"{cpuText} {ppuText}");
        }*/

        _ticks++;
    }

    public void AddCpu(IClockHook cpu) => _callbacks[0] = cpu;

    public void AddPpu(IClockHook ppu) => _callbacks[1] = ppu;

    public void Run()
    {
        try
        {
            while (! Aborted)
            {
                Tick();

                if (HangUp())
                {
                    Aborted = true;
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

    public void Abort() => Aborted = true;
}