namespace NesCs.Logic.Clocking;

public class NullClockHook : IClockHook
{
    public int MasterClockDivisor => 0;

    public string GetStatus() => string.Empty;

    public bool Trigger(IClock clock) => false;
}