using NesCs.Logic.Cpu;

namespace NesCs.Logic.Ram;

public interface IClockHook
{
    int MasterClockDivisor { get; }
    bool Trigger(IClock clock);
    string GetStatus();
}