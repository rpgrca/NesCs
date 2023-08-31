namespace NesCs.Logic.Cpu.Clocking;

public interface IClockHook
{
    int MasterClockDivisor { get; }
    bool Trigger(IClock clock);
    string GetStatus();
}