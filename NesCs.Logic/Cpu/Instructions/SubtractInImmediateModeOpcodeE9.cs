using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class SubtractInImmediateModeOpcodeE9 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromAccumulator();
        var (result, overflow) = CalculateSub(cpu, a, value);

        cpu.SetValueIntoAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
 
        if (overflow)
        {
            cpu.SetOverflowFlag();
            cpu.ClearCarryFlag();
        }
        else
        {
            cpu.ClearOverflowFlag();
        }
    }

    private (byte Result, bool Overflow) CalculateSub(Cpu6502 cpu, byte minuend, byte subtrahend)
    {
        var result = (byte)(minuend - subtrahend - (cpu.ReadCarryFlag() == ProcessorStatus.None? 1 : 0));
        var operandSign = (((ProcessorStatus)subtrahend & ProcessorStatus.N) == ProcessorStatus.N)? 1 : 0;
        var carryFlag = (cpu.ReadCarryFlag() == ProcessorStatus.C)? 1 : 0;
        var resultSign = (((ProcessorStatus)result & ProcessorStatus.N) == ProcessorStatus.N)? 1 : 0;
        return (result, operandSign != carryFlag && operandSign == resultSign);
    }
}