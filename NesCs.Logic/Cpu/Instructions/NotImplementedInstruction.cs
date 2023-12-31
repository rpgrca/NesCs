namespace NesCs.Logic.Cpu.Instructions;

internal class NotImplementedInstruction : IInstruction
{
    private readonly int _opcode;
    private readonly string _message;

    public NotImplementedInstruction(int opcode, string message = "Unknown opcode")
    {
        _opcode = opcode;
        _message = message;
    }

    public string Name => "NI";

    public byte Opcode => 0x00;

    string IDebuggerDisplay.Display => "Not implemented";

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu) =>
        throw new NotImplementedException($"Not implemented opcode {_opcode:X2}: {_message}");
}