using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

public class DummyRamController : IRamController
{
    public byte this[int index] { get => 0; set { } }

    public void Copy(byte[] program, int startIndex, int memoryOffset, int programSize)
    {
    }
}