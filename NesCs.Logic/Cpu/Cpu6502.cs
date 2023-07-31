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

    private Cpu6502(byte[] program, int start, int end, int pc, byte a, byte x, byte y, byte s, ProcessorStatus p, byte[] ram, IInstruction[] instructions, List<(int, byte, string)> trace)
    {
        _program = program;
        _start = start;
        _end = end;
        PC = pc;
        A = a;
        X = x;
        Y = y;
        S = s;
        P = p;
        _ram = ram;
        _instructions = instructions;
        _trace = trace;
        _ip = 0;
    }

/*
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
    }*/

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

    internal int ReadByteFromProgramCounter() => PC;

    internal byte ReadByteFromStackPointer() => S;

    internal byte ReadByteFromAccumulator() => A;

    internal ProcessorStatus ReadCarryFlag() => P & ProcessorStatus.C;

    internal bool ReadZeroFlag() => (P & ProcessorStatus.Z) == ProcessorStatus.Z;

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

    internal void SetValueIntoRegisterX(byte value) => X = value;

    internal void SetValueIntoRegisterY(byte value) => Y = value;

    internal void SetValueIntoStackPointer(byte value) => S = value;

    internal void SetZeroFlagBasedOn(byte value)
    {
        if (value == 0)
        {
            P |= ProcessorStatus.Z;
        }
        else
        {
            P &= ~ProcessorStatus.Z;
        }
    }

    internal void SetNegativeFlagBasedOn(byte value)
    {
        if (((ProcessorStatus)value & ProcessorStatus.N) == ProcessorStatus.N)
        {
            P |= ProcessorStatus.N;
        }
        else
        {
            ClearNegativeFlag();
        }
    }

    internal void SetOverflowFlagBasedOn(byte value)
    {
        if (((ProcessorStatus)value & ProcessorStatus.V) == ProcessorStatus.V)
        {
            P |= ProcessorStatus.V;
        }
        else
        {
            P &= ~ProcessorStatus.V;
        }
    }

    internal void SetOverflowFlag() => P |= ProcessorStatus.V;

    internal void SetCarryFlag() => P |= ProcessorStatus.C;

    internal void SetZeroFlag() => P |= ProcessorStatus.Z;

    internal void SetNegativeFlag() => P |= ProcessorStatus.N;

    internal void SetDecimalFlag() => P |= ProcessorStatus.D;

    internal void SetInterruptDisable() => P |= ProcessorStatus.I;

    internal void ClearNegativeFlag() => P &= ~ProcessorStatus.N;

    internal void ClearCarryFlag() => P &= ~ProcessorStatus.C;

    internal void ClearDecimalMode() => P &= ~ProcessorStatus.D;

    internal void ClearInterruptDisable() => P &= ~ProcessorStatus.I;

    internal void ClearOverflowFlag() => P &= ~ProcessorStatus.V;

    internal void ClearZeroFlag() => P &= ~ProcessorStatus.Z;

    private void Trace(int pc, byte value, string type) => _trace.Add((pc, value, type));

    public byte PeekMemory(int address) => _ram[address];

    public (ProcessorStatus P, byte A, int PC, byte X, byte Y, byte S) TakeSnapshot() => (P, A, PC, X, Y, S);
}