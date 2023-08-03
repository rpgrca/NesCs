namespace NesCs.Logic;

public interface ITracer
{
    void Write(int address, byte value);
    void Read(int address, byte value);
}
