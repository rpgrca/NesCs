using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class RamControllerSpy : IRamController
{
    public byte[] Ram { get; set; }

    public byte this[int index] { get => Ram[index]; set => Ram[index] = value; }

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