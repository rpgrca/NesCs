namespace NesCs.Logic.Cpu.Instructions;

public class NotImplementedInstruction : IInstruction
{
    private readonly int _opcode;
    private readonly string _message;

    public NotImplementedInstruction(int opcode, string message = "Unknown opcode")
    {
        _opcode = opcode;
        _message = message;
    }

    public void Execute(Cpu6502 cpu) =>
        throw new NotImplementedException($"Not implemented opcode {_opcode:X}: {_message}");
}