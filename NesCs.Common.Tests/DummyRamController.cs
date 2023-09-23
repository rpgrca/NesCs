using NesCs.Logic.Ram;

namespace NesCs.Common.Tests;

public class DummyRamController : IRamController
{
    public byte this[int index] { get => 0; set { } }

    public void Copy(byte[] program, int startIndex, int memoryOffset, int programSize)
    {
    }

    public byte DirectReadFrom(int index) => 0;

    public void DirectWriteTo(int index, byte value)
    {
    }

    public void RegisterHook(IRamHook hook)
    {
    }
}