using System.Diagnostics;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

[DebuggerDisplay("{_cycles}")]
public class Clock : IClock
{
    private int _ticks;
    private bool _stopped;
    private readonly List<IClockHook> _callbacks;

    public Clock(int ticks)
    {
        _ticks = ticks;
        _stopped = false;
        _callbacks = new List<IClockHook>();
    }

    public int GetCycles() => _ticks;

    private bool HangUp() => _ticks > 30_000_000;

    private void Tick()
    {
        _ticks++;
        foreach (var callback in _callbacks)
        {
            if (_ticks % callback.MasterClockDivisor == 0)
            {
                callback.Trigger(_ticks);
            }
        }
    }

    public void AddCallback(IClockHook hook) => _callbacks.Add(hook);

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