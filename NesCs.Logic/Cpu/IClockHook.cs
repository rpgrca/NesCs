namespace NesCs.Logic.Ram;

public interface IClockHook
{
    int MasterClockDivisor { get; }
    void Trigger(int tick);
}