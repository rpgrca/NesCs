using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

public interface IClock
{
    int GetCycles();
    void AddCpu(IClockHook hook);
    void AddPpu(IClockHook hook);
    void Run();
    void Abort();
    bool Aborted { get; }
}