using System.Diagnostics;

namespace NesCs.Logic.Clocking;

[DebuggerDisplay("{_ticks}")]
public class Clock : IClock
{
    private int _ticks;
    private readonly int _maximum;
    private IClockHook[] _callbacks;

    public bool Aborted { get; private set; }

    public Clock(int ticks)
        : this(ticks, 100_000_000)
    {
    }

    public Clock(int ticks, int maximum)
    {
        _ticks = ticks;
        _maximum = maximum;
        _callbacks = new IClockHook[2] { null, null };
        Aborted = false;
    }

    public int GetCycles() => _ticks;

    private bool HangUp()
    {
        if (_ticks > _maximum)
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

        _callbacks[1]?.Trigger(this);
        var canRefresh = _callbacks[0].Trigger(this);

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