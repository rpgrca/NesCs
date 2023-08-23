namespace NesCs.Logic.Cpu;

public class Clock : IClock
{
    private int _cycles;

    public Clock(int cycles) => _cycles = cycles;

    public int GetCycles() => _cycles;

    public bool HangUp() => _cycles > 30_000_000;

    public void Tick() => _cycles++;
}
