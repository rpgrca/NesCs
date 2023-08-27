using System.Diagnostics;
using NesCs.Logic.Cpu.Instructions;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Cpu;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public partial class Cpu6502 : IClockHook
{
    private const int StackMemoryBase = 0x0100;
    private ProcessorStatus P { get; set; }
    private byte A { get; set; }
    private int PC { get; set; }
    private byte X { get; set; }
    private byte Y { get; set; }
    private byte S { get; set; }
    private int _previousCycles;
    private int _cycles;
    private bool _initialized;
    private readonly int _resetVector;
    private readonly int _nmiVector;
    private readonly int _irqVector;
    private readonly IRamController _ram;
    private readonly IInstruction[] _instructions;
    private readonly ITracer _tracer;
    private readonly IClock _clock;
    private readonly Dictionary<int, Action<Cpu6502>> _callbacks;

    private Cpu6502(byte[] program, int programSize, IRamController ramController, int[] memoryOffsets, int pc, byte a, byte x, byte y, byte s, ProcessorStatus p, IClock clock, (int Address, byte Value)[] ramPatches, IInstruction[] instructions, ITracer tracer, Dictionary<int, Action<Cpu6502>> callbacks, int resetVector, int nmiVector, int irqVector)
    {
        _callbacks = callbacks;
        _ram = ramController;
        foreach (var memoryOffset in memoryOffsets)
        {
            _ram.Copy(program, 0, memoryOffset, programSize);
        }

        foreach (var (address, value) in ramPatches)
        {
            _ram[address] = value;
        }

        _resetVector = resetVector;
        _nmiVector = nmiVector;
        _irqVector = irqVector;
        PC = pc;
        A = a;
        X = x;
        Y = y;
        S = s;
        P = p;
        _clock = clock;
        _clock.AddCpu(this);
        _cycles = _previousCycles = _clock.GetCycles() % MasterClockDivisor;
        _instructions = instructions;
        _tracer = tracer;
    }

    public void PowerOn()
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

        if (PC == 0)
        {
            var low = ReadByteFromMemory(_resetVector);
            var high = ReadByteFromMemory(_resetVector + 1);
            var address = high << 8 | low;
            SetValueToProgramCounter(address);
        }
    }

    public void Step()
    {
        _previousCycles = _cycles;

        if (_callbacks.ContainsKey(PC))
        {
            _callbacks[PC].Invoke(this);
        }

        if (! _clock.Aborted)
        {
            var instruction = _instructions[ReadByteFromProgram()];
            _tracer.Display(instruction, instruction.PeekOperands(this), PC, A, X, Y, P, S, _previousCycles);
            instruction.Execute(this);
        }
    }

    public void Stop() => _clock.Abort();

    public void Run() => _clock.Run();

    public void Reset()
    {
        SetInterruptDisable();

        var sp = ReadByteFromStackPointer();
        sp -= 3;
        SetValueToStackPointer(sp);

        var low = ReadByteFromMemory(_resetVector);
        var high = ReadByteFromMemory(_resetVector + 1);

        var address = high << 8 | low;
        SetValueToProgramCounter(address);
    }

    internal byte ReadByteFromRegisterY() => Y;

    internal byte ReadByteFromRegisterX() => X;

    internal int ReadByteFromProgramCounter() => PC;

    public byte ReadByteFromStackPointer() => S;

    public byte ReadByteFromAccumulator() => A;

    internal bool IsCarryFlagSet() => P.HasFlag(ProcessorStatus.C);

    internal bool IsZeroFlagSet() => P.HasFlag(ProcessorStatus.Z);

    internal bool IsNegativeFlagSet() => P.HasFlag(ProcessorStatus.N);

    internal bool IsOverflowFlagSet() => P.HasFlag(ProcessorStatus.V);

    internal void ReadyForNextInstruction() => PC = (PC + 1) & 0xffff;

    internal byte ReadByteFromProgram()
    {
        var value = _ram[PC];
        _tracer.Read(PC, value);
        _cycles++;
        return value;
    }

    public byte ReadByteFromMemory(int address)
    {
        var value = _ram[address];
        _tracer.Read(address, value);
        _cycles++;
        return value;
    }

    public void WriteByteToMemory(int address, byte value)
    {
        _ram[address] = value;
        _tracer.Write(address, value);
        _cycles++;
    }

    // TODO: Deberia aumentar el puntero automaticamente
    public byte ReadByteFromStackMemory()
    {
        var address = StackMemoryBase + S;
        var value = _ram[address];
        _tracer.Read(address, value);
        _cycles++;
        return value;
    }

    public byte PopFromStack()
    {
        var address = StackMemoryBase + S + 1;
        var value = _ram[address];
        _tracer.Read(address, value);
        S += 1;
        _cycles++;
        return value;
    }

    internal void WriteByteToStackMemory(byte value)
    {
        var address = StackMemoryBase + S;
        _ram[address] = value;
        _tracer.Write(address, value);
        _cycles++;
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

    public byte PeekMemory(int address) => _ram[address & 0xffff];

    public (ProcessorStatus P, byte A, int PC, byte X, byte Y, byte S) TakeSnapshot() => (P, A, PC, X, Y, S);

    public bool Trigger(IClock clock)
    {
        if (! _initialized)
        {
            _cycles = 7;
            _previousCycles = 0;
            _initialized = true;
            return false;
        }
        else
        {
            if (clock.GetCycles() % 12 == 0)
            {
                _previousCycles++;

                if (_previousCycles == _cycles)
                {
                    Step();
                    return true;
                }
            }
        }

        return false;
    }

    public string GetStatus() => DebuggerDisplay;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"{PC:X4} A:{A:X2} X:{X:X2} Y:{Y:X2} P:{(byte)P:X2} S:{S:X2} CYC:{_cycles}";

    public int MasterClockDivisor => 12;
}