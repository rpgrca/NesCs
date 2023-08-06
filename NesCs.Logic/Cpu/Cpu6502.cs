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
    private int _cycles;
    private int _counter;
    private bool _stopped;
    private readonly int _start;
    private readonly int _end;
    private readonly byte[] _ram;
    private readonly IInstruction[] _instructions;
    private readonly ITracer _tracer;
    private readonly Dictionary<int, Action<Cpu6502>> _callbacks;

    private Cpu6502(byte[] program, int programSize, int ramSize, int[] memoryOffsets, int start, int end, int pc, byte a, byte x, byte y, byte s, ProcessorStatus p, int cycles, (int Address, byte Value)[] ramPatches, IInstruction[] instructions, ITracer tracer, Dictionary<int, Action<Cpu6502>> callbacks)
    {
        _callbacks = callbacks;
        _ram = new byte[ramSize];

        foreach (var memoryOffset in memoryOffsets)
        {
            Array.Copy(program, 0, _ram, memoryOffset, programSize);
        }

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
        _stopped = false;
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

    public void Step()
    {
        if (_callbacks.ContainsKey(PC))
        {
            _callbacks[PC].Invoke(this);
        }

        var opcode = ReadByteFromProgram();
        _tracer.Display(opcode, PC, A, X, Y, P, S, _cycles);
        _instructions[opcode].Execute(this);
    }

    public void Stop() => _stopped = true;

    public void Run()
    {
        try
        {
            while (! _stopped)
            {
                Step();
            }
        }
        catch (Exception ex)
        {
            var error = $"{ex.Message} (on cycle {_cycles})";
            Console.WriteLine(error);
            System.Diagnostics.Debug.Print(error);
            throw;
        }
    }

    internal byte ReadByteFromRegisterY() => Y;

    internal byte ReadByteFromRegisterX() => X;

    internal int ReadByteFromProgramCounter() => PC;

    public byte ReadByteFromStackPointer() => S;

    public byte ReadByteFromAccumulator() => A;

    internal bool IsReadCarryFlagSet() => P.HasFlag(ProcessorStatus.C);

    internal bool IsReadZeroFlagSet() => P.HasFlag(ProcessorStatus.Z);

    internal bool IsNegativeFlagSet() => P.HasFlag(ProcessorStatus.N);

    internal bool IsOverflowFlagSet() => P.HasFlag(ProcessorStatus.V);

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

    public byte ReadByteFromMemory(int address)
    {
        var value = _ram[address];
        _tracer.Read(address, value);
        return value;
    }

    public void WriteByteToMemory(int address, byte value)
    {
        _ram[address] = value;
        _tracer.Write(address, value);
    }

    // TODO: Deberia aumentar el puntero automaticamente
    public byte ReadByteFromStackMemory()
    {
        var address = StackMemoryBase + S;
        var value = _ram[address];
        _tracer.Read(address, value);
        return value;
    }

    public byte PopFromStack()
    {
        var address = StackMemoryBase + S + 1;
        var value = _ram[address];
        _tracer.Read(address, value);
        S += 1;
        return value;
    }

    internal void WriteByteToStackMemory(byte value)
    {
        var address = StackMemoryBase + S;
        _ram[address] = value;
        _tracer.Write(address, value);
        S -= 1;
    }

    public void SetValueToAccumulator(byte value) => A = value;

    internal void SetValueToRegisterX(byte value) => X = value;

    internal void SetValueToRegisterY(byte value) => Y = value;

    public void SetValueToStackPointer(byte value) => S = value;

    public void SetValueToProgramCounter(int value) => PC = value;

    internal ProcessorStatus GetFlags() => P;

    internal void OverwriteFlags(ProcessorStatus flags) => P = flags;

    internal void SetZeroFlagBasedOn(byte value)
    {
        if (value == 0)
        {
            SetZeroFlag();
        }
        else
        {
            ClearZeroFlag();
        }
    }

    internal void SetNegativeFlagBasedOn(byte value)
    {
        if (((ProcessorStatus)value).HasFlag(ProcessorStatus.N))
        {
            SetNegativeFlag();
        }
        else
        {
            ClearNegativeFlag();
        }
    }

    internal void SetOverflowFlagBasedOn(byte value)
    {
        if (((ProcessorStatus)value).HasFlag(ProcessorStatus.V))
        {
            SetOverflowFlag();
        }
        else
        {
            ClearOverflowFlag();
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