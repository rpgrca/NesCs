using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    private const int StackMemoryBase = 0x0100;
    private ProcessorStatus P { get; set; }
    private byte A { get; set; }
    private int PC { get; set; }
    private byte X { get; set; }
    private byte Y { get; set; }
    private byte S { get; set; }
    private readonly int _start;
    private readonly int _end;
    private readonly byte[] _ram;
    private int _cycles;
    private int _counter;
    private readonly IInstruction[] _instructions;
    private readonly ITracer _tracer;

    private Cpu6502(byte[] program, int programSize, int ramSize, int memoryOffset, int start, int end, int pc, byte a, byte x, byte y, byte s, ProcessorStatus p, int cycles, (int Address, byte Value)[] ramPatches, IInstruction[] instructions, ITracer tracer)
    {
        _ram = new byte[ramSize];
        Array.Copy(program, 0, _ram, memoryOffset, programSize);

        foreach (var (address, value) in ramPatches)
        {
            _ram[address] = value;
        }

        _start = start;
        _end = end;
        PC = pc;
        A = a;
        X = x;
        Y = y;
        S = s;
        P = p;
        _cycles = cycles;
        _counter = 0;
        _instructions = instructions;
        _tracer = tracer;
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
        try
        {
            _counter = _start;
            while (_counter++ < _end || _start == _end)
            {
                if (_cycles == 11357)
                {
                    System.Diagnostics.Debugger.Break();
                }
                var opcode = ReadByteFromProgram();
                _tracer.Display(opcode, PC, A, X, Y, P, S, _cycles);

                _instructions[opcode].Execute(this);
            }
        }
        catch (Exception ex)
        {
            var error = $"{ex.Message} (on cycle {_counter})";
            Console.WriteLine(error);
            System.Diagnostics.Debug.Print(error);
            throw;
        }
    }

    internal byte ReadByteFromRegisterY() => Y;

    internal byte ReadByteFromRegisterX() => X;

    internal int ReadByteFromProgramCounter() => PC;

    internal byte ReadByteFromStackPointer() => S;

    internal byte ReadByteFromAccumulator() => A;

    internal bool ReadCarryFlag() => (P & ProcessorStatus.C) == ProcessorStatus.C;

    internal bool ReadZeroFlag() => (P & ProcessorStatus.Z) == ProcessorStatus.Z;

    internal bool ReadNegativeFlag() => (P & ProcessorStatus.N) == ProcessorStatus.N;

    internal bool ReadOverflowFlag() => (P & ProcessorStatus.V) == ProcessorStatus.V;

    internal void ReadyForNextInstruction()
    {
        PC = (PC + 1) & 0xffff;
        _cycles++;
    }

    internal byte ReadByteFromProgram()
    {
        var value = _ram[PC - _start];
        _tracer.Read(PC, value);
        return value;
    }

    internal byte ReadByteFromMemory(int address)
    {
        var value = _ram[address];
        _tracer.Read(address, value);
        return value;
    }

    internal void WriteByteToMemory(int address, byte value)
    {
        _ram[address] = value;
        _tracer.Write(address, value);
    }

    internal byte ReadByteFromStackMemory()
    {
        var address = StackMemoryBase + S;
        var value = _ram[address];
        _tracer.Read(address, value);
        return value;
    }

    internal void WriteByteToStackMemory(byte value)
    {
        var address = StackMemoryBase + S;
        _ram[address] = value;
        _tracer.Write(address, value);
        S -= 1;
    }

    internal void SetValueIntoAccumulator(byte value) => A = value;

    internal void SetValueIntoRegisterX(byte value) => X = value;

    internal void SetValueIntoRegisterY(byte value) => Y = value;

    internal void SetValueIntoStackPointer(byte value) => S = value;

    internal void SetValueIntoProgramCounter(int value) => PC = value;

    internal ProcessorStatus GetFlags() => P;

    internal void SetFlags(ProcessorStatus flags) => P = flags;

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

    public byte PeekMemory(int address) => _ram[address];

    public (ProcessorStatus P, byte A, int PC, byte X, byte Y, byte S) TakeSnapshot() => (P, A, PC, X, Y, S);
}