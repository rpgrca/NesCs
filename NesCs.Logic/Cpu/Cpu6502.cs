using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    private ProcessorStatus P { get; set; }
    private byte A { get; set; }
    private int PC { get; set; }
    private byte X { get; set; }
    private byte Y { get; set; }
    private byte S { get; set; }
    private readonly byte[] _program;
    private int _start;
    private int _end;
    private byte[] _ram;
    private int _ip;
    private readonly List<(int, byte, string)> _trace;
    private IInstruction[] _instructions;

    private Cpu6502(byte[] program, int start, int end, int pc, byte a, byte x, byte y, byte s, byte p, byte[] ram, List<(int, byte, string)> trace)
    {
        _program = program;
        _start = start;
        _end = end;
        PC = pc;
        A = a;
        X = x;
        Y = y;
        S = s;
        P = (ProcessorStatus)p;
        _ram = ram;
        _trace = trace;
        _ip = 0;

        _instructions = new IInstruction[0x100];
        for (var index = 0; index < 0x100; index++)
        {
            _instructions[index] = new NotImplementedInstruction();
        }

        _instructions[0xA5] = new LdaInZeroPageModeOpcodeA5();
        _instructions[0xA9] = new LdaInImmediateModeOpcodeA9();
        _instructions[0xAD] = new LdaInAbsoluteModeOpcodeAD();
        _instructions[0xB1] = new LdaInIndirectYModeOpcodeB1();
        _instructions[0xB5] = new LdaInZeroPageXModeOpcodeB5();
        _instructions[0xB9] = new LdaInAbsoluteYModeOpcodeB9();
        _instructions[0xBD] = new LdaInAbsoluteXModeOpcodeBD();
    }

    private void PowerOn()
    {
        A = X = Y = 0;
        P = (ProcessorStatus)0x34;
        S = 0xFD;
        _ram[0x4017] = 0x00;
        _ram[0x4015] = 0x00;

        int index;
        for (index = 0x4000; index <= 0x400F; index++)
        {
            _ram[index] = 0x00;
        }

        for (index = 0x4010; index <= 0x4013; index++)
        {
            _ram[index] = 0x00;
        }
    }

    public void Run()
    {
        _ip = _start;
        while (_ip < _end)
        {
            var opcode = ReadByteFromProgram();
            _instructions[opcode].Execute(this);
        }
    }

    internal byte ReadByteFromRegisterY() => Y;

    internal byte ReadByteFromRegisterX() => X;

    internal void ReadyForNextInstruction() => PC = (PC + 1) & 0xffff;

    internal byte ReadByteFromProgram()
    {
        var value = _program[_ip++];
        Trace(PC, value, "read");
        return value;
    }

    internal byte ReadByteFromMemory(int address)
    {
        var value = _ram[address];
        Trace(address, value, "read");
        return value;
    }

    internal void SetValueIntoAccumulator(byte value) => A = value;

    internal void SetZeroFlagBasedOnAccumulator()
    {
        if (A == 0)
        {
            P |= ProcessorStatus.Z;
        }
        else
        {
            P &= ~ProcessorStatus.Z;
        }
    }

    internal void SetNegativeFlagBasedOnAccumulator()
    {
        if (((ProcessorStatus)A & ProcessorStatus.N) == ProcessorStatus.N)
        {
            P |= ProcessorStatus.N;
        }
        else
        {
            P &= ~ProcessorStatus.N;
        }
    }

    private void ClearNegativeFlag() => P &= ~ProcessorStatus.N;

    private void SetCarryFlag() => P |= ProcessorStatus.C;

    private void Trace(int pc, byte value, string type) => _trace.Add((pc, value, type));

    public byte PeekMemory(int address) => _ram[address];

    public (ProcessorStatus P, byte A, int PC, byte X, byte Y, byte S) TakeSnapshot() => (P, A, PC, X, Y, S);
}