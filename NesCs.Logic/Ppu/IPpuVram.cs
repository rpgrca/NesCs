namespace NesCs.Logic.Ppu;

public interface IPpuVram
{
    int CurrentAddress { get; }
    void Write(byte value);
    byte Read();
    void IncrementAddress();
}