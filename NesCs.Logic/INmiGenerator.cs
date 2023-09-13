namespace NesCs.Logic.Cpu;

public interface INmiGenerator
{
    void SetControl(byte value);
    void SetStatus(byte value);
}