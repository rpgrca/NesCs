namespace NesCs.Logic.Ram;

public interface IRamHook
{
    void Write(byte value);
    byte Read(int index);
    bool CanHandle(int index);
}