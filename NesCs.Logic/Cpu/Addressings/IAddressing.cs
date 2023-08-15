namespace NesCs.Logic.Cpu.Addressings;

public interface IAddressing
{
    (int Address, byte Value) ObtainValueAndAddress(Cpu6502 cpu) => throw new NotImplementedException();
    byte[] PeekOperands(Cpu6502 cpu);
}