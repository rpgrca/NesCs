namespace NesCs.Logic.Ram;

public interface IClockHook
{
    int MasterClockDivisor { get; }
    bool Trigger(int tick);
    string GetStatus();
}