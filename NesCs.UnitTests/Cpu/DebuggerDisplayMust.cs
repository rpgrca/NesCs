using NesCs.Logic.Cpu;

namespace NesCs.UnitTests.Cpu;

public class DebuggerDisplayMust
{
    [Theory]
    [InlineData(0x00, "BRK", "(imp)")]
    [InlineData(0x01, "ORA", "(izx)")]
    [InlineData(0x05, "ORA", "(zp)")]
    [InlineData(0x06, "ASL", "(zp)")]
    [InlineData(0x08, "PHP", "(imp)")]
    [InlineData(0x09, "ORA", "(imm)")]
    [InlineData(0x0A, "ASL", "(imp)")]
    [InlineData(0x0D, "ORA", "(abs)")]
    [InlineData(0x0E, "ASL", "(abs)")]
    [InlineData(0x10, "BPL", "(rel)")]
    [InlineData(0x11, "ORA", "(izy)")]
    [InlineData(0x15, "ORA", "(zpx)")]
    [InlineData(0x16, "ASL", "(zpx)")]
    [InlineData(0x18, "CLC", "(imp)")]
    [InlineData(0x19, "ORA", "(aby)")]
    [InlineData(0x1D, "ORA", "(abx)")]
    [InlineData(0x1E, "ASL", "(abx)")]
    [InlineData(0x20, "JSR", "(abs)")]
    [InlineData(0x21, "AND", "(izx)")]
    [InlineData(0x24, "BIT", "(zp)")]
    [InlineData(0x25, "AND", "(zp)")]
    [InlineData(0x26, "ROL", "(zp)")]
    [InlineData(0x28, "PLP", "(imp)")]
    [InlineData(0x29, "AND", "(imm)")]
    [InlineData(0x2A, "ROL", "(imp)")]
    [InlineData(0x2C, "BIT", "(abs)")]
    [InlineData(0x2D, "AND", "(abs)")]
    [InlineData(0x2E, "ROL", "(abs)")]
    [InlineData(0x30, "BMI", "(rel)")]
    [InlineData(0x31, "AND", "(izy)")]
    [InlineData(0x35, "AND", "(zpx)")]
    [InlineData(0x36, "ROL", "(zpx)")]
    [InlineData(0x38, "SEC", "(imp)")]
    [InlineData(0x39, "AND", "(aby)")]
    [InlineData(0x3D, "AND", "(abx)")]
    [InlineData(0x3E, "ROL", "(abx)")]
    [InlineData(0x40, "RTI", "(imp)")]
    [InlineData(0x41, "EOR", "(izx)")]
    [InlineData(0x45, "EOR", "(zp)")]
    [InlineData(0x46, "LSR", "(zp)")]
    [InlineData(0x48, "PHA", "(imp)")]
    [InlineData(0x49, "EOR", "(imm)")]
    [InlineData(0x4A, "LSR", "(imp)")]
    [InlineData(0x4C, "JMP", "(abs)")]
    [InlineData(0x4D, "EOR", "(abs)")]
    [InlineData(0x4E, "LSR", "(abs)")]
    [InlineData(0x50, "BVC", "(rel)")]
    [InlineData(0x51, "EOR", "(izy)")]
    [InlineData(0x55, "EOR", "(zpx)")]
    [InlineData(0x56, "LSR", "(zpx)")]
    [InlineData(0x58, "CLI", "(imp)")]
    [InlineData(0x59, "EOR", "(aby)")]
    [InlineData(0x5D, "EOR", "(abx)")]
    [InlineData(0x5E, "LSR", "(abx)")]
    [InlineData(0x60, "RTS", "(imp)")]
    [InlineData(0x61, "ADC", "(izx)")]
    [InlineData(0x65, "ADC", "(zp)")]
    [InlineData(0x66, "ROR", "(zp)")]
    [InlineData(0x68, "PLA", "(imp)")]
    [InlineData(0x69, "ADC", "(imm)")]
    [InlineData(0x6A, "ROR", "(imp)")]
    [InlineData(0x6C, "JMP", "(ind)")]
    [InlineData(0x6D, "ADC", "(abs)")]
    [InlineData(0x6E, "ROR", "(abs)")]
    [InlineData(0x70, "BVS", "(rel)")]
    [InlineData(0x71, "ADC", "(izy)")]
    [InlineData(0x75, "ADC", "(zpx)")]
    [InlineData(0x76, "ROR", "(zpx)")]
    [InlineData(0x78, "SEI", "(imp)")]
    [InlineData(0x79, "ADC", "(aby)")]
    [InlineData(0x7D, "ADC", "(abx)")]
    [InlineData(0x7E, "ROR", "(abx)")]
    [InlineData(0x81, "STA", "(izx)")]
    public void Test1(byte opcode, string expectedInstruction, string expectedAddressing)
    {
        var spy = new InstructionTracerSpy();
        var sut = new Cpu6502.Builder()
            .Running(new byte[] { opcode, 0x00, 0x00 })
            .ProgramMappedAt(0x00)
            .TracingWith(spy)
            .Build();

        sut.Step();

        Assert.Contains(expectedInstruction, spy.Instruction.Display);
        Assert.EndsWith(expectedAddressing, spy.Instruction.Display);
    }
}