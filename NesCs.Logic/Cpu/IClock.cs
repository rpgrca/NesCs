namespace NesCs.Logic.Cpu;

public interface IClock
{
    void Tick();
    int GetCycles();
    bool HangUp();
}
