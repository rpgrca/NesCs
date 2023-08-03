namespace NesCs.Logic.Cpu;

public class DummyTracer : ITracer
{
    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}
