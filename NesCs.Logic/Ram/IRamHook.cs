namespace NesCs.Logic.Ram;

public interface IRamHook
{
    void Call(int index, byte value, byte[] ram);
    bool CanHandle(int index);
}