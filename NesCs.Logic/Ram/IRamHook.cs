namespace NesCs.Logic.Ram;

public interface IRamHook
{
    void Write(int index, byte value);
    void WriteDma(byte value);
    byte Read(int index);
    bool CanHandle(int index);
}
