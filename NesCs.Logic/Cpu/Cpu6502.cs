namespace NesCs.Logic.Cpu;

public class Cpu6502
{
    public class Builder
    {
        private byte _p;
        private byte _a;
        private int _pc;
        private byte _x;
        private byte _y;
        private byte _s;
        private byte[] _program;
        private byte[] _ram;
        private int _ramSize;
        private int[][] _patch;
        private int _start;
        private int _end;
        private List<(int, byte, string)> _trace;

        public Builder()
        {
            _p = _a = _x = _y = _s = 0;
            _ramSize =_start = _end = _pc = 0;
            _program = _ram = Array.Empty<byte>();
            _patch = Array.Empty<int[]>();
            _trace = new();
        }

        public Builder RunningProgram(byte[] program)
        {
            _program = program;
            return this;
        }

        public Builder StartingAt(int start)
        {
            _start = start;
            return this;
        }

        public Builder EndingAt(int end)
        {
            _end = end;
            return this;
        }

        public Builder WithRamSizeOf(int size)
        {
            _ramSize = size;
            return this;
        }

        public Builder WithAccumulatorAs(byte a)
        {
            _a = a;
            return this;
        }

        public Builder WithStackPointerAt(byte s)
        {
            _s = s;
            return this;
        }

        public Builder WithProcessorStatusAs(byte p)
        {
            _p = p;
            return this;
        }

        public Builder WithXAs(byte x)
        {
            _x = x;
            return this;
        }

        public Builder WithYAs(byte y)
        {
            _y = y;
            return this;
        }

        public Builder WithProgramCounterAs(int pc)
        {
            _pc = pc;
            return this;
        }

        public Builder RamPatchedAs(int[][] patch)
        {
            _patch = patch;
            return this;
        }

        public Builder TracingWith(List<(int, byte, string)> trace)
        {
            _trace = trace;
            return this;
        }

        public Cpu6502 Build()
        {
            _patch ??= Array.Empty<int[]>();
            if (_ramSize < 1) _ramSize = 0xFFFF;
            if (_ram.Length < 1) _ram = new byte[_ramSize];
            if (_end < 1) _end = _program.Length;

            foreach ((int address, byte value) in _patch.Select(v => ((int)v[0], (byte)v[1])))
            {
                _ram[address] = value;
            }

            return new Cpu6502(_program, _start, _end, _pc, _a, _x, _y, _s, _p, _ram, _trace);
        }
    }

    [Flags]
    public enum SRFlags
    {
        C = 1 << 0,
        Z = 1 << 1,
        I = 1 << 2,
        D = 1 << 3,
        B = 1 << 4,
        X = 1 << 5,
        V = 1 << 6,
        N = 1 << 7
    }

    public SRFlags P { get; private set; }
    public byte A { get; private set; }
    public int PC { get; private set; }
    public byte X { get; private set; }
    public byte Y { get; private set; }
    public byte S { get; private set; }
    private readonly byte[] _program;
    private int _start;
    private int _end;
    private byte[] _ram;
    private int _ip;
    private readonly List<(int, byte, string)> _trace;

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
        P = (SRFlags)p;
        _ram = ram;
        _trace = trace;
        _ip = 0;
    }

    [Obsolete("Replaced by builder")]
    public Cpu6502(byte[] program, int start, int end)
    {
        _program = program;
        _start = start;
        _end = end;
        PC = 0;
        A = 0;
        _ram = new byte[0xFFFF];

        PowerOn();
    }

    private void PowerOn()
    {
        A = X = Y = 0;
        P = (SRFlags)0x34;
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
        byte address, value;

        _ip = _start;
        while (_ip < _end)
        {
            var opcode = _program[_ip++];
            Trace(PC, opcode, "read");
            PC++;

            switch (opcode)
            {
                // LDA Load Accumulator with Memory
                // (indirect),Y   LDA (oper),Y   B1   2   5* 
                // OPC ($LL),Y	operand is zeropage address; effective address is word in (LL, LL + 1) incremented by Y with carry: C.w($00LL) + Y
                case 0xB1:
                    address = _program[_ip++];
                    Trace(PC, address, "read");

                    var low = _ram[address];
                    Trace(address, low, "read");

                    var high = _ram[address + 1];
                    Trace(address + 1, high, "read");

                    A = _ram[PC];
                    Trace(PC, A, "read");
                    PC++;

                    var effectiveAddress = ((high << 8) | low) + Y;
                    A = _ram[effectiveAddress];
                    Trace(effectiveAddress, A, "read");

                    SetZeroFlagBasedOnAccumulator();
                    ClearNegativeFlag();
                    break;


                // zeropage,X	LDA oper,X	B5	2	4
                case 0xB5:
                    address = _program[_ip++];
                    Trace(PC, address, "read");

                    value = _ram[address];
                    Trace(address, value, "read");

                    PC += 1;
                    value = (byte)(address + X);
                    A = _ram[value];
                    Trace(value, A, "read");

                    SetZeroFlagBasedOnAccumulator();
                    SetNegativeFlagBasedOnAccumulator();
                    break;

                default:
                    throw new ArgumentException($"Opcode {opcode} not handled");
            }
        }
    }

    private void SetZeroFlagBasedOnAccumulator()
    {
        if (A == 0)
        {
            P |= SRFlags.Z;
        }
        else
        {
            P &= ~SRFlags.Z;
        }
    }

    private void SetNegativeFlagBasedOnAccumulator()
    {
        if (((SRFlags)A & SRFlags.N) == SRFlags.N)
        {
            P |= SRFlags.N;
        }
        else
        {
            P &= ~SRFlags.N;
        }
    }

    private void ClearNegativeFlag() => P &= ~SRFlags.N;

    private void Trace(int pc, byte value, string type) =>
        _trace.Add((pc, value, type));

    public byte PeekMemory(int address) => _ram[address];
}