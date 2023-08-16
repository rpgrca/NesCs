namespace NesCs.Logic.Ram;

public interface IRamController
{
    byte this[int index] { get; set; }

    void Copy(byte[] program, int startIndex, int memoryOffset, int programSize);
}