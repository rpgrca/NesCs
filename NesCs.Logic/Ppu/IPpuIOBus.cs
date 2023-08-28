namespace NesCs.Logic.Ppu;

public interface IPpuIOBus
{
    void Write(byte value);
    byte Read();
    void Clear();
}