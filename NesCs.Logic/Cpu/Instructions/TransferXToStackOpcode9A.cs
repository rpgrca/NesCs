namespace NesCs.Logic.Cpu.Instructions;

// https://ryukojiro.github.io/v6502/isa.html#isa_tsx is wrong, TSX does not affect any flags per answer in
// https://retrocomputing.stackexchange.com/questions/15695/does-the-6502s-txs-and-tsx-affect-flags-or-not
public class TransferXToStackOpcode9A : IInstruction
{
    public string Name => "TXS";

    public byte Opcode => 0x9A;

    public void Execute(Cpu6502 cpu)
    {
        var value = cpu.ReadByteFromRegisterX();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.SetValueToStackPointer(value);
    }
}