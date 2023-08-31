namespace NesCs.Logic.Cpu.Operations;

internal class Xor : IOperation
{
    public (int, byte) Execute(Cpu6502 cpu, byte value, int address)
    {
        var result = (byte)(cpu.ReadByteFromAccumulator() ^ value);
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);

        return (address, result);
    }
}