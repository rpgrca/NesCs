namespace NesCs.Logic.Cpu.Instructions;

public class StoreAccumulatorZeroPageXOpcode95 : IInstruction
{
    public string Name => "STA";

    public byte Opcode => 0x95;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address += cpu.ReadByteFromRegisterX();
 
        cpu.ReadyForNextInstruction();
        cpu.WriteByteToMemory(address, cpu.ReadByteFromAccumulator());
    }
}