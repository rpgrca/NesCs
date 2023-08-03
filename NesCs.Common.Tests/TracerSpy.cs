using NesCs.Logic;

public class TracerSpy : ITracer
{
    private List<(int, byte, string)> _trace;

    public TracerSpy(List<(int, byte, string)> trace) => _trace = trace;

    public void Write(int address, byte value) => _trace.Add((address, value, "write"));

    public void Read(int address, byte value) => _trace.Add((address, value, "read"));
}

