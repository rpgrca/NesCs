using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

public interface IClock
{
    int GetCycles();
    void AddCallback(IClockHook hook);
    void Run();
}
