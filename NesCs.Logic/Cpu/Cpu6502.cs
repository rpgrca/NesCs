namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    public ProcessorStatus P { get; private set; }
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
        P = (ProcessorStatus)p;
        _ram = ram;
        _trace = trace;
        _ip = 0;
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
        byte address, value;

        _ip = _start;
        while (_ip < _end)
        {
            var opcode = _program[_ip++];
            Trace(PC, opcode, "read");
            PC++;

            switch (opcode)
            {
                // LDA Load Accumulator with Memory (Indirect), Y
                // (indirect),Y   LDA (oper),Y   B1   2   5* 
                // OPC ($LL),Y	operand is zeropage address; effective address is word in (LL, LL + 1) incremented by Y with carry: C.w($00LL) + Y
                case 0xB1:
                    address = ReadByteFromProgram();
                    var low = ReadByteFromMemory(address);

                    address = (byte)((address + 1) & 0xff);
                    var high = ReadByteFromMemory(address);

                    var effectiveAddress = (high << 8) | ((low + Y) & 0xff);
                    A = ReadByteFromMemory(effectiveAddress);
                    PC++;

                    var effectiveAddress2 = (((high << 8) | low) + Y) & 0xffff;
                    if (effectiveAddress != effectiveAddress2)
                    {
                        A = ReadByteFromMemory(effectiveAddress2);
                    }

                    SetZeroFlagBasedOnAccumulator();
                    SetNegativeFlagBasedOnAccumulator();
                    break;


                // zeropage,X	LDA oper,X	B5	2	4
                case 0xB5:
                    address = ReadByteFromProgram();
                    _ = ReadByteFromMemory(address);

                    PC += 1;
                    A = ReadByteFromMemory((byte)(address + X));

                    SetZeroFlagBasedOnAccumulator();
                    SetNegativeFlagBasedOnAccumulator();
                    break;

                default:
                    throw new ArgumentException($"Opcode {opcode} not handled");
            }
        }
    }

    private byte ReadByteFromProgram()
    {
        var address = _program[_ip++];
        Trace(PC, address, "read");
        return address;
    }

    private byte ReadByteFromMemory(int address)
    {
        var low = _ram[address];
        Trace(address, low, "read");
        return low;
    }

    private void SetZeroFlagBasedOnAccumulator()
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

    private void SetNegativeFlagBasedOnAccumulator()
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

    private void Trace(int pc, byte value, string type) =>
        _trace.Add((pc, value, type));

    public byte PeekMemory(int address) => _ram[address];
}