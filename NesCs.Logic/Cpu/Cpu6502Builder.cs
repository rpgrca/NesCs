using NesCs.Logic.Cpu.Instructions;
using NesCs.Logic.Clocking;
using NesCs.Logic.Ram;
using NesCs.Logic.Tracing;

namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    public class Builder
    {
        private ProcessorStatus _p;
        private byte _a, _x, _y, _s;
        private int _pc, _programSize, _cycles, _nmiVector, _resetVector, _irqVector;
        private byte[] _program;
        private readonly List<int> _mappedProgramAddresses;
        private (int Address, byte Value)[] _patch;
        private readonly IInstruction[] _instructions;
        private readonly Dictionary<int, Action<Cpu6502, IInstruction>> _callbacks;
        private ITracer _tracer;
        private IClock? _clock;
        private bool _enableInvalid;
        private readonly Addressings.Addressings As;
        private readonly Operations.Operations Doing;
        private IRamController? _ramController;
        private int _divisor;
        private IDmaCopier _dmaCopier;

        public Builder()
        {
            _enableInvalid = false;
            _nmiVector = 0xFFFA;
            _resetVector = 0xFFFC;
            _irqVector = 0xFFFE;
            _mappedProgramAddresses = new List<int>();
            _callbacks = new Dictionary<int, Action<Cpu6502, IInstruction>>();
            _p = ProcessorStatus.None;
            _a = _x = _y = _s = 0;
            _pc = _programSize = _cycles = 0;
            _program = Array.Empty<byte>();
            _patch = Array.Empty<(int, byte)>();
            _tracer = new DummyTracer();
            _divisor = 12;
            _dmaCopier = new DummyDmaCopier();
            As = new Addressings.Addressings();
            Doing = new Operations.Operations();

            _instructions = new IInstruction[0x100];
            for (var index = 0; index < 0x100; index++)
            {
                _instructions[index] = new NotImplementedInstruction(index);
            }

            _instructions[0x00] = new ForceInterruptOpcode00();
            _instructions[0x01] = new Instruction(0x01, "ORA", As.IndirectXIndexed.Memory, Doing.Or);
            _instructions[0x05] = new Instruction(0x05, "ORA", As.ZeroPage.Memory, Doing.Or);
            _instructions[0x06] = new Instruction(0x06, "ASL", As.ZeroPage.Memory, Doing.ShiftLeft.Memory);
            _instructions[0x08] = new PushProcessorStatusOpcode08();
            _instructions[0x09] = new Instruction(0x09, "ORA", As.Immediate, Doing.Or);
            _instructions[0x0A] = new Instruction(0x0A, "ASL", As.Accumulator, Doing.ShiftLeft.Accumulator);
            _instructions[0x0D] = new Instruction(0x0D, "ORA", As.Absolute.Memory, Doing.Or);
            _instructions[0x0E] = new Instruction(0x0E, "ASL", As.Absolute.Memory, Doing.ShiftLeft.Memory);
            _instructions[0x10] = new Instruction(0x10, "BPL", As.Relative, Doing.Branch.WhenPositive);
            _instructions[0x11] = new Instruction(0x11, "ORA", As.IndirectYIndexed.Memory, Doing.Or);
            _instructions[0x15] = new Instruction(0x15, "ORA", As.ZeroPageXIndexed.Memory, Doing.Or);
            _instructions[0x16] = new Instruction(0x16, "ASL", As.ZeroPageXIndexed.Memory, Doing.ShiftLeft.Memory);
            _instructions[0x18] = new Instruction(0x18, "CLC", As.Implied.Memory, Doing.Flag.Minus.C);
            _instructions[0x19] = new Instruction(0x19, "ORA", As.AbsoluteYIndexed.Common, Doing.Or);
            _instructions[0x1D] = new Instruction(0x1D, "ORA", As.AbsoluteXIndexed.Common, Doing.Or);
            _instructions[0x1E] = new Instruction(0x1E, "ASL", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.ShiftLeft.Memory);
            _instructions[0x20] = new JumpToSubroutineOpcode20();
            _instructions[0x21] = new Instruction(0x21, "AND", As.IndirectXIndexed.Memory, Doing.And.Accumulator);
            _instructions[0x24] = new Instruction(0x24, "BIT", As.ZeroPage.Memory, Doing.BitTest);
            _instructions[0x25] = new Instruction(0x25, "AND", As.ZeroPage.Memory, Doing.And.Accumulator);
            _instructions[0x26] = new Instruction(0x26, "ROL", As.ZeroPage.Memory, Doing.RotateLeft.OnMemory);
            _instructions[0x28] = new PullProcessorStatusOpcode28();
            _instructions[0x29] = new Instruction(0x29, "AND", As.Immediate, Doing.And.Accumulator);
            _instructions[0x2A] = new Instruction(0x2A, "ROL", As.Accumulator, Doing.RotateLeft.OnAccumulator);
            _instructions[0x2C] = new Instruction(0x2C, "BIT", As.Absolute.Memory, Doing.BitTest);
            _instructions[0x2D] = new Instruction(0x2D, "AND", As.Absolute.Memory, Doing.And.Accumulator);
            _instructions[0x2E] = new Instruction(0x2E, "ROL", As.Absolute.Memory, Doing.RotateLeft.OnMemory);
            _instructions[0x30] = new Instruction(0x30, "BMI", As.Relative, Doing.Branch.WhenNegative);
            _instructions[0x31] = new Instruction(0x31, "AND", As.IndirectYIndexed.Memory, Doing.And.Accumulator);
            _instructions[0x35] = new Instruction(0x35, "AND", As.ZeroPageXIndexed.Memory, Doing.And.Accumulator);
            _instructions[0x36] = new Instruction(0x36, "ROL", As.ZeroPageXIndexed.Memory, Doing.RotateLeft.OnMemory);
            _instructions[0x38] = new Instruction(0x38, "SEC", As.Implied.Memory, Doing.Flag.Plus.C);
            _instructions[0x39] = new Instruction(0x39, "AND", As.AbsoluteYIndexed.Common, Doing.And.Accumulator);
            _instructions[0x3D] = new Instruction(0x3D, "AND", As.AbsoluteXIndexed.Common, Doing.And.Accumulator);
            _instructions[0x3E] = new Instruction(0x3E, "ROL", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.RotateLeft.OnMemory);
            _instructions[0x40] = new ReturnFromInterruptOpcode40();
            _instructions[0x41] = new Instruction(0x41, "EOR", As.IndirectXIndexed.Memory, Doing.Xor);
            _instructions[0x45] = new Instruction(0x45, "EOR", As.ZeroPage.Memory, Doing.Xor);
            _instructions[0x46] = new Instruction(0x46, "LSR", As.ZeroPage.Memory, Doing.ShiftRight.Memory);
            _instructions[0x48] = new PushAccumulatorOpcode48();
            _instructions[0x49] = new Instruction(0x49, "EOR", As.Immediate, Doing.Xor);
            _instructions[0x4A] = new Instruction(0x4A, "LSR", As.Accumulator, Doing.ShiftRight.Accumulator);
            _instructions[0x4C] = new Instruction(0x4C, "JMP", As.Absolute.Direct, Doing.Jump);
            _instructions[0x4D] = new Instruction(0x4D, "EOR", As.Absolute.Memory, Doing.Xor);
            _instructions[0x4E] = new Instruction(0x4E, "LSR", As.Absolute.Memory, Doing.ShiftRight.Memory);
            _instructions[0x50] = new Instruction(0x50, "BVC", As.Relative, Doing.Branch.WhenNotOverflow);
            _instructions[0x51] = new Instruction(0x51, "EOR", As.IndirectYIndexed.Memory, Doing.Xor);
            _instructions[0x55] = new Instruction(0x55, "EOR", As.ZeroPageXIndexed.Memory, Doing.Xor);
            _instructions[0x56] = new Instruction(0x56, "LSR", As.ZeroPageXIndexed.Memory, Doing.ShiftRight.Memory);
            _instructions[0x58] = new Instruction(0x58, "CLI", As.Implied.Memory, Doing.Flag.Minus.I);
            _instructions[0x59] = new Instruction(0x59, "EOR", As.AbsoluteYIndexed.Common, Doing.Xor);
            _instructions[0x5D] = new Instruction(0x5D, "EOR", As.AbsoluteXIndexed.Common, Doing.Xor);
            _instructions[0x5E] = new Instruction(0x5E, "LSR", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.ShiftRight.Memory);
            _instructions[0x60] = new ReturnFromSubroutineOpcode60();
            _instructions[0x61] = new Instruction(0x61, "ADC", As.IndirectXIndexed.Memory, Doing.AddWithCarry);
            _instructions[0x65] = new Instruction(0x65, "ADC", As.ZeroPage.Memory, Doing.AddWithCarry);
            _instructions[0x66] = new Instruction(0x66, "ROR", As.ZeroPage.Memory, Doing.RotateRight.OnMemory);
            _instructions[0x68] = new PullAccumulatorOpcode68();
            _instructions[0x69] = new Instruction(0x69, "ADC", As.Immediate, Doing.AddWithCarry);
            _instructions[0x6A] = new Instruction(0x6A, "ROR", As.Accumulator, Doing.RotateRight.OnAccumulator);
            _instructions[0x6C] = new Instruction(0x6C, "JMP", As.Indirect, Doing.Jump);
            _instructions[0x6D] = new Instruction(0x6D, "ADC", As.Absolute.Memory, Doing.AddWithCarry);
            _instructions[0x6E] = new Instruction(0x6E, "ROR", As.Absolute.Memory, Doing.RotateRight.OnMemory);
            _instructions[0x70] = new Instruction(0x70, "BVS", As.Relative, Doing.Branch.WhenOverflow);
            _instructions[0x71] = new Instruction(0x71, "ADC", As.IndirectYIndexed.Memory, Doing.AddWithCarry);
            _instructions[0x75] = new Instruction(0x75, "ADC", As.ZeroPageXIndexed.Memory, Doing.AddWithCarry);
            _instructions[0x76] = new Instruction(0x76, "ROR", As.ZeroPageXIndexed.Memory, Doing.RotateRight.OnMemory);
            _instructions[0x78] = new Instruction(0x78, "SEI", As.Implied.Memory, Doing.Flag.Plus.I);
            _instructions[0x79] = new Instruction(0x79, "ADC", As.AbsoluteYIndexed.Common, Doing.AddWithCarry);
            _instructions[0x7D] = new Instruction(0x7D, "ADC", As.AbsoluteXIndexed.Common, Doing.AddWithCarry);
            _instructions[0x7E] = new Instruction(0x7E, "ROR", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.RotateRight.OnMemory);
            _instructions[0x81] = new Instruction(0x81, "STA", As.IndirectXIndexed.Accumulator, Doing.Store.Memory);
            _instructions[0x84] = new Instruction(0x84, "STY", As.ZeroPage.Y, Doing.Store.Memory);
            _instructions[0x85] = new Instruction(0x85, "STA", As.ZeroPage.Accumulator, Doing.Store.Memory);
            _instructions[0x86] = new Instruction(0x86, "STX", As.ZeroPage.X, Doing.Store.Memory);
            _instructions[0x88] = new Instruction(0x88, "DEY", As.Implied.Memory, Doing.Decrement.Y);
            _instructions[0x8A] = new Instruction(0x8A, "TXA", As.Implied.X, Doing.Transfer.Accumulator);
            _instructions[0x8C] = new Instruction(0x8C, "STY", As.Absolute.Y, Doing.Store.Memory);
            _instructions[0x8D] = new Instruction(0x8D, "STA", As.Absolute.Accumulator, Doing.Store.Memory);
            _instructions[0x8E] = new Instruction(0x8E, "STX", As.Absolute.X, Doing.Store.Memory);
            _instructions[0x90] = new Instruction(0x90, "BCC", As.Relative, Doing.Branch.WhenNotCarry);
            _instructions[0x91] = new Instruction(0x91, "STA", As.IndirectYIndexed.Accumulator, Doing.Store.Memory);
            _instructions[0x94] = new Instruction(0x94, "STY", As.ZeroPageXIndexed.Y, Doing.Store.Memory);
            _instructions[0x95] = new Instruction(0x95, "STA", As.ZeroPageXIndexed.Accumulator, Doing.Store.Memory);
            _instructions[0x96] = new Instruction(0x96, "STX", As.ZeroPageYIndexed.X, Doing.Store.Memory);
            _instructions[0x98] = new Instruction(0x98, "TXS", As.Implied.Y, Doing.Transfer.Accumulator);
            _instructions[0x99] = new Instruction(0x99, "STA", As.AbsoluteYIndexed.Accumulator, Doing.Store.Memory);
            _instructions[0x9A] = new Instruction(0x9A, "TXS", As.Implied.X, Doing.Store.Stack);
            _instructions[0x9D] = new Instruction(0x9D, "STA", As.AbsoluteXIndexed.Accumulator, Doing.Store.Memory);
            _instructions[0xA0] = new Instruction(0xA0, "LDY", As.Immediate, Doing.Load.Y);
            _instructions[0xA1] = new Instruction(0xA1, "LDA", As.IndirectXIndexed.Memory, Doing.Load.Accumulator);
            _instructions[0xA2] = new Instruction(0xA2, "LDX", As.Immediate, Doing.Load.X);
            _instructions[0xA4] = new Instruction(0xA4, "LDY", As.ZeroPage.Memory, Doing.Load.Y);
            _instructions[0xA5] = new Instruction(0xA5, "LDA", As.ZeroPage.Memory, Doing.Load.Accumulator);
            _instructions[0xA6] = new Instruction(0xA6, "LDX", As.ZeroPage.Memory, Doing.Load.X);
            _instructions[0xA8] = new Instruction(0xA8, "TAY", As.Implied.Accumulator, Doing.Transfer.Y);
            _instructions[0xA9] = new Instruction(0xA9, "LDA", As.Immediate, Doing.Load.Accumulator);
            _instructions[0xAA] = new Instruction(0xAA, "TAX", As.Accumulator, Doing.Load.X);
            _instructions[0xAC] = new Instruction(0xAC, "LDY", As.Absolute.Memory, Doing.Load.Y);
            _instructions[0xAD] = new Instruction(0xAD, "LDA", As.Absolute.Memory, Doing.Load.Accumulator);
            _instructions[0xAE] = new Instruction(0xAE, "LDX", As.Absolute.Memory, Doing.Load.X);
            _instructions[0xB0] = new Instruction(0xB0, "BCS", As.Relative, Doing.Branch.WhenCarry);
            _instructions[0xB1] = new Instruction(0xB1, "LDA", As.IndirectYIndexed.Memory, Doing.Load.Accumulator);
            _instructions[0xB4] = new Instruction(0xB4, "LDY", As.ZeroPageXIndexed.Memory, Doing.Load.Y);
            _instructions[0xB5] = new Instruction(0xB5, "LDA", As.ZeroPageXIndexed.Memory, Doing.Load.Accumulator);
            _instructions[0xB6] = new Instruction(0xB6, "LDX", As.ZeroPageYIndexed.Memory, Doing.Load.X);
            _instructions[0xB8] = new Instruction(0xB8, "CLV", As.Implied.Memory, Doing.Flag.Minus.V);
            _instructions[0xB9] = new Instruction(0xB9, "LDA", As.AbsoluteYIndexed.Common, Doing.Load.Accumulator);
            _instructions[0xBA] = new Instruction(0xBA, "TSX", As.Implied.Stack, Doing.Transfer.X);
            _instructions[0xBC] = new Instruction(0xBC, "LDY", As.AbsoluteXIndexed.Common, Doing.Load.Y);
            _instructions[0xBD] = new Instruction(0xBD, "LDA", As.AbsoluteXIndexed.Common, Doing.Load.Accumulator);
            _instructions[0xBE] = new Instruction(0xBE, "LDX", As.AbsoluteYIndexed.Common, Doing.Load.X);
            _instructions[0xC0] = new Instruction(0xC0, "CMP", As.Immediate, Doing.Compare.Y);
            _instructions[0xC1] = new Instruction(0xC1, "CMP", As.IndirectXIndexed.Memory, Doing.Compare.Accumulator);
            _instructions[0xC4] = new Instruction(0xC4, "CMP", As.ZeroPage.Memory, Doing.Compare.Y);
            _instructions[0xC5] = new Instruction(0xC5, "CMP", As.ZeroPage.Memory, Doing.Compare.Accumulator);
            _instructions[0xC6] = new Instruction(0xC6, "DEC", As.ZeroPage.Memory, Doing.Decrement.Memory);
            _instructions[0xC8] = new Instruction(0xC8, "INY", As.Implied.Memory, Doing.Increment.Y);
            _instructions[0xC9] = new Instruction(0xC9, "CMP", As.Immediate, Doing.Compare.Accumulator);
            _instructions[0xCA] = new Instruction(0xCA, "DEX", As.Implied.Memory, Doing.Decrement.X);
            _instructions[0xCC] = new Instruction(0xCC, "CMP", As.Absolute.Memory, Doing.Compare.Y);
            _instructions[0xCD] = new Instruction(0xCD, "CMP", As.Absolute.Memory, Doing.Compare.Accumulator);
            _instructions[0xCE] = new Instruction(0xCE, "DEC", As.Absolute.Memory, Doing.Decrement.Memory);
            _instructions[0xD0] = new Instruction(0xD0, "BNE", As.Relative, Doing.Branch.WhenNotZero);
            _instructions[0xD1] = new Instruction(0xD1, "CMP", As.IndirectYIndexed.Memory, Doing.Compare.Accumulator);
            _instructions[0xD5] = new Instruction(0xD5, "CMP", As.ZeroPageXIndexed.Memory, Doing.Compare.Accumulator);
            _instructions[0xD6] = new Instruction(0xD6, "DEC", As.ZeroPageXIndexed.Memory, Doing.Decrement.Memory);
            _instructions[0xD8] = new Instruction(0xD8, "CLD", As.Implied.Memory, Doing.Flag.Minus.D);
            _instructions[0xD9] = new Instruction(0xD9, "CMP", As.AbsoluteYIndexed.Common, Doing.Compare.Accumulator);
            _instructions[0xDD] = new Instruction(0xDD, "CMP", As.AbsoluteXIndexed.Common, Doing.Compare.Accumulator);
            _instructions[0xDE] = new Instruction(0xDE, "DEC", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.Decrement.Memory);
            _instructions[0xE0] = new Instruction(0xE0, "CMP", As.Immediate, Doing.Compare.X);
            _instructions[0xE1] = new Instruction(0xE1, "SBC", As.IndirectXIndexed.Memory, Doing.SubtractWithCarry);
            _instructions[0xE4] = new Instruction(0xE4, "CMP", As.ZeroPage.Memory, Doing.Compare.X);
            _instructions[0xE5] = new Instruction(0xE5, "SBC", As.ZeroPage.Memory, Doing.SubtractWithCarry);
            _instructions[0xE6] = new Instruction(0xE6, "INC", As.ZeroPage.Memory, Doing.Increment.Memory);
            _instructions[0xE8] = new Instruction(0xE8, "INX", As.Implied.Memory, Doing.Increment.X);
            _instructions[0xE9] = new Instruction(0xE9, "SBC", As.Immediate, Doing.SubtractWithCarry);
            _instructions[0xEA] = new Instruction(0xEA, "NOP", As.Implied.Memory, Doing.Nop);
            _instructions[0xEC] = new Instruction(0xEC, "CMP", As.Absolute.Memory, Doing.Compare.X);
            _instructions[0xED] = new Instruction(0xED, "SBC", As.Absolute.Memory, Doing.SubtractWithCarry);
            _instructions[0xEE] = new Instruction(0xEE, "INC", As.Absolute.Memory, Doing.Increment.Memory);
            _instructions[0xF0] = new Instruction(0xF0, "BEQ", As.Relative, Doing.Branch.WhenZero);
            _instructions[0xF1] = new Instruction(0xF1, "SBC", As.IndirectYIndexed.Memory, Doing.SubtractWithCarry);
            _instructions[0xF5] = new Instruction(0xF5, "SBC", As.ZeroPageXIndexed.Memory, Doing.SubtractWithCarry);
            _instructions[0xF6] = new Instruction(0xF6, "INC", As.ZeroPageXIndexed.Memory, Doing.Increment.Memory);
            _instructions[0xF8] = new Instruction(0xF8, "SED", As.Implied.Memory, Doing.Flag.Plus.D);
            _instructions[0xF9] = new Instruction(0xF9, "SBC", As.AbsoluteYIndexed.Common, Doing.SubtractWithCarry);
            _instructions[0xFD] = new Instruction(0xFD, "SBC", As.AbsoluteXIndexed.Common, Doing.SubtractWithCarry);
            _instructions[0xFE] = new Instruction(0xFE, "INC", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.Increment.Memory);
        }

        public Builder Running(byte[] program)
        {
            _program = program;
            return this;
        }

        public Builder WithClock(IClock clock)
        {
            _clock = clock;
            return this;
        }

        public Builder WithClockDivisorOf(int divider)
        {
            _divisor = divider;
            return this;
        }

        public Builder SupportingInvalidInstructions()
        {
            _enableInvalid = true;
            return this;
        }

        public Builder WithSizeOf(int size)
        {
            _programSize = size;
            return this;
        }

        public Builder ProgramMappedAt(int imageStart)
        {
            _mappedProgramAddresses.Add(imageStart);
            return this;
        }

        public Builder WithResetVectorAt(int resetVector)
        {
            _resetVector = resetVector;
            return this;
        }

        public Builder WithNmiVectorAt(int nmiVector)
        {
            _nmiVector = nmiVector;
            return this;
        }

        public Builder WithIrqVectorAt(int irqVector)
        {
            _irqVector = irqVector;
            return this;
        }

        public Builder WithRamController(IRamController ramController)
        {
            _ramController = ramController;
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

        public Builder WithProcessorStatusAs(ProcessorStatus p)
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

        public Builder RamPatchedAs((int, byte)[] patch)
        {
            _patch = patch;
            return this;
        }

        public Builder TracingWith(ITracer tracer)
        {
            _tracer = tracer;
            return this;
        }

        public Builder WithDmaCopier(IDmaCopier dmaCopier)
        {
            _dmaCopier = dmaCopier;
            return this;
        }

        public Builder WithCallback(int address, Action<Cpu6502, IInstruction> callback)
        {
            _callbacks.Add(address, callback);
            return this;
        }

        public Cpu6502 Build()
        {
            _patch ??= Array.Empty<(int, byte)>();
            if (_programSize < 1) _programSize = _program.Length;
            if (_enableInvalid) AddInvalidOpcodes();
            _clock ??= new Clock(_cycles);
            _ramController ??= new RamController.Builder().Build();
            _dmaCopier ??= new DummyDmaCopier();

            return new Cpu6502(_program, _programSize, _ramController, _mappedProgramAddresses.ToArray(),
                _pc, _a, _x, _y, _s, _p, _clock, _patch, _instructions, _tracer, _callbacks,
                _resetVector, _nmiVector, _irqVector, _divisor, _dmaCopier);
        }

        private void AddInvalidOpcodes()
        {
            //_instructions[0x02] R
            _instructions[0x03] = new IllegalInstruction(0x03, "SLO", As.IndirectXIndexed.Memory, Doing.ShiftLeft.Memory, Doing.Or);
            _instructions[0x04] = new Instruction(0x04, "IGN", As.ZeroPage.Memory, Doing.Nop);
            _instructions[0x07] = new IllegalInstruction(0x07, "SLO", As.ZeroPage.Memory, Doing.ShiftLeft.Memory, Doing.Or);
            //_instructions[0x0B] R
            _instructions[0x0C] = new Instruction(0x0C, "IGN", As.Absolute.Memory, Doing.Nop);
            _instructions[0x0F] = new IllegalInstruction(0x0F, "SLO", As.Absolute.Memory, Doing.ShiftLeft.Memory, Doing.Or);
            //_instructions[0x12] R
            _instructions[0x13] = new IllegalInstruction(0x13, "SLO", As.IndirectYIndexed.DoubleMemoryRead, Doing.ShiftLeft.Memory, Doing.Or);
            _instructions[0x14] = new Instruction(0x14, "IGN", As.ZeroPageXIndexed.Memory, Doing.Nop);
            _instructions[0x17] = new IllegalInstruction(0x17, "SLO", As.ZeroPageXIndexed.Memory, Doing.ShiftLeft.Memory, Doing.Or);
            _instructions[0x1A] = new Instruction(0x1A, "NOP*", As.Implied.Memory, Doing.Nop);
            _instructions[0x1B] = new IllegalInstruction(0x1B, "SLO", As.AbsoluteYIndexed.DoubleMemoryRead, Doing.ShiftLeft.Memory, Doing.Or);
            _instructions[0x1C] = new Instruction(0x1C, "IGN", As.AbsoluteXIndexed.Common, Doing.Nop);
            _instructions[0x1F] = new IllegalInstruction(0x1F, "SLO", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.ShiftLeft.Memory, Doing.Or);
            //_instructions[0x22] R
            _instructions[0x23] = new IllegalInstruction(0x23, "RLA", As.IndirectXIndexed.Memory, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            _instructions[0x27] = new IllegalInstruction(0x27, "RLA", As.ZeroPage.Memory, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            //_instructions[0x2B] R
            _instructions[0x2F] = new IllegalInstruction(0x2F, "RLA", As.Absolute.Memory, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            //_instructions[0x32] R
            _instructions[0x33] = new IllegalInstruction(0x33, "RLA", As.IndirectYIndexed.DoubleMemoryRead, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            _instructions[0x34] = new Instruction(0x34, "IGN", As.ZeroPageXIndexed.Memory, Doing.Nop);
            _instructions[0x37] = new IllegalInstruction(0x37, "RLA", As.ZeroPageXIndexed.Memory, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            _instructions[0x3A] = new Instruction(0x3A, "NOP*", As.Implied.Memory, Doing.Nop);
            _instructions[0x3B] = new IllegalInstruction(0x3B, "RLA", As.AbsoluteYIndexed.DoubleMemoryRead, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            _instructions[0x3C] = new Instruction(0x3C, "IGN", As.AbsoluteXIndexed.Common, Doing.Nop);
            _instructions[0x3F] = new IllegalInstruction(0x3F, "RLA", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.RotateLeft.OnMemory, Doing.And.Accumulator);
            //_instructions[0x42] R
            _instructions[0x43] = new IllegalInstruction(0x43, "SRE", As.IndirectXIndexed.Memory, Doing.ShiftRight.Memory, Doing.Xor);
            _instructions[0x44] = new Instruction(0x44, "IGN", As.ZeroPage.Memory, Doing.Nop);
            _instructions[0x47] = new IllegalInstruction(0x47, "SRE", As.ZeroPage.Memory, Doing.ShiftRight.Memory, Doing.Xor);
            //_instructions[0x4B] R
            _instructions[0x4F] = new IllegalInstruction(0x4F, "SRE", As.Absolute.Memory, Doing.ShiftRight.Memory, Doing.Xor);
            //_instructions[0x52] R
            _instructions[0x53] = new IllegalInstruction(0x53, "SRE", As.IndirectYIndexed.DoubleMemoryRead, Doing.ShiftRight.Memory, Doing.Xor);
            _instructions[0x54] = new Instruction(0x54, "IGN", As.ZeroPageXIndexed.Memory, Doing.Nop);
            _instructions[0x57] = new IllegalInstruction(0x57, "SRE", As.ZeroPageXIndexed.Memory, Doing.ShiftRight.Memory, Doing.Xor);
            _instructions[0x5A] = new Instruction(0x5A, "NOP*", As.Implied.Memory, Doing.Nop);
            _instructions[0x5B] = new IllegalInstruction(0x5B, "SRE", As.AbsoluteYIndexed.DoubleMemoryRead, Doing.ShiftRight.Memory, Doing.Xor);
            _instructions[0x5C] = new Instruction(0x5C, "IGN", As.AbsoluteXIndexed.Common, Doing.Nop);
            _instructions[0x5F] = new IllegalInstruction(0x5F, "SRE", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.ShiftRight.Memory, Doing.Xor);
            //_instructions[0x62] R
            _instructions[0x63] = new IllegalInstruction(0x63, "RRA", As.IndirectXIndexed.Memory, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            _instructions[0x64] = new Instruction(0x64, "IGN", As.ZeroPage.Memory, Doing.Nop);
            _instructions[0x67] = new IllegalInstruction(0x67, "RRA", As.ZeroPage.Memory, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            //_instructions[0x6B] R
            _instructions[0x6F] = new IllegalInstruction(0x6F, "RRA", As.Absolute.Memory, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            //_instructions[0x72] R
            _instructions[0x73] = new IllegalInstruction(0x73, "RRA", As.ZeroPageYIndexed.DoubleMemoryRead, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            _instructions[0x74] = new Instruction(0x74, "IGN", As.ZeroPageXIndexed.Memory, Doing.Nop);
            _instructions[0x77] = new IllegalInstruction(0x77, "RRA", As.ZeroPageXIndexed.Memory, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            _instructions[0x7A] = new Instruction(0x7A, "NOP*", As.Implied.Memory, Doing.Nop);
            _instructions[0x7B] = new IllegalInstruction(0x7B, "RRA", As.AbsoluteYIndexed.DoubleMemoryRead, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            _instructions[0x7C] = new Instruction(0x7C, "IGN", As.AbsoluteXIndexed.Common, Doing.Nop);
            _instructions[0x7F] = new IllegalInstruction(0x7F, "RRA", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.RotateRight.OnMemory, Doing.AddWithCarry);
            _instructions[0x80] = new Instruction(0x80, "SKB", As.Immediate, Doing.Nop);
            _instructions[0x82] = new Instruction(0x82, "SKB", As.Immediate, Doing.Nop);
            _instructions[0x83] = new Instruction(0x83, "SAX", As.IndirectXIndexed.X, Doing.And.Memory);
            _instructions[0x87] = new Instruction(0x87, "SAX", As.ZeroPage.X, Doing.And.Memory);
            _instructions[0x89] = new Instruction(0x89, "SKB", As.Immediate, Doing.Nop);
            //_instructions[0x8B] R XAA, ANE
            _instructions[0x8F] = new Instruction(0x8F, "SAX", As.Absolute.X, Doing.And.Memory);
            //_instructions[0x92] R
            //_instructions[0x93] W
            _instructions[0x97] = new Instruction(0x97, "SAX", As.ZeroPageYIndexed.X, Doing.And.Memory);
            //_instructions[0x9B] W
            //_instructions[0x9C] W
            //_instructions[0x9E] W
            //_instructions[0x9F] W
            _instructions[0xA3] = new IllegalInstruction(0xA3, "LAX", As.IndirectXIndexed.Memory, Doing.Load.Accumulator, Doing.Load.X);
            _instructions[0xA7] = new IllegalInstruction(0xA7, "LAX", As.ZeroPage.Memory, Doing.Load.Accumulator, Doing.Load.X);
            //_instructions[0xAB] R
            _instructions[0xAF] = new IllegalInstruction(0xAF, "LAX", As.Absolute.Memory, Doing.Load.Accumulator, Doing.Load.X);
            //_instructions[0xB2] R
            _instructions[0xB3] = new IllegalInstruction(0xB3, "LAX", As.IndirectYIndexed.Memory, Doing.Load.Accumulator, Doing.Load.X);
            _instructions[0xB7] = new IllegalInstruction(0xB7, "LAX", As.ZeroPageYIndexed.Memory, Doing.Load.Accumulator, Doing.Load.X);
            //_instructions[0xBB] R
            _instructions[0xBF] = new IllegalInstruction(0xBF, "LAX", As.AbsoluteYIndexed.Common, Doing.Load.Accumulator, Doing.Load.X);
            _instructions[0xC2] = new Instruction(0xC2, "SKB", As.Immediate, Doing.Nop);
            _instructions[0xC3] = new IllegalInstruction(0xC3, "DCP", As.IndirectXIndexed.Memory, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            _instructions[0xC7] = new IllegalInstruction(0xC7, "DCP", As.ZeroPage.Memory, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            //_instructions[0xCB] R AXS / SBX
            _instructions[0xCF] = new IllegalInstruction(0xCF, "DCP", As.Absolute.Memory, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            //_instructions[0xD2] R
            _instructions[0xD3] = new IllegalInstruction(0xD3, "DCP", As.IndirectYIndexed.DoubleMemoryRead, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            _instructions[0xD4] = new Instruction(0xD4, "IGN", As.ZeroPageXIndexed.Memory, Doing.Nop);
            _instructions[0xD7] = new IllegalInstruction(0xD7, "DCP", As.ZeroPageXIndexed.Memory, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            _instructions[0xDA] = new Instruction(0xDA, "NOP*", As.Implied.Memory, Doing.Nop);
            _instructions[0xDB] = new IllegalInstruction(0xDB, "DCP", As.AbsoluteYIndexed.DoubleMemoryRead, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            _instructions[0xDC] = new Instruction(0xDC, "IGN", As.AbsoluteXIndexed.Common, Doing.Nop);
            _instructions[0xDF] = new IllegalInstruction(0xDF, "DCP", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.Decrement.Memory, Doing.Compare.Accumulator);
            _instructions[0xE2] = new Instruction(0xE2, "NOP*", As.Immediate, Doing.Nop);
            _instructions[0xE3] = new IllegalInstruction(0xE3, "ISC", As.IndirectXIndexed.Memory, Doing.Increment.Memory, Doing.SubtractWithCarry);
            _instructions[0xE7] = new IllegalInstruction(0xE7, "ISC", As.ZeroPage.Memory, Doing.Increment.Memory, Doing.SubtractWithCarry);
            _instructions[0xEB] = new Instruction(0xEB, "SBC", As.Immediate, Doing.SubtractWithCarry);
            _instructions[0xEF] = new IllegalInstruction(0xEF, "ISC", As.Absolute.Memory, Doing.Increment.Memory, Doing.SubtractWithCarry);
            //_instructions[0xF2] R
            _instructions[0xF3] = new IllegalInstruction(0xF3, "DCP", As.IndirectXIndexed.DoubleMemoryRead, Doing.Increment.Memory, Doing.SubtractWithCarry);
            _instructions[0xF4] = new Instruction(0xF4, "IGN", As.ZeroPageXIndexed.Memory, Doing.Nop);
            _instructions[0xF7] = new IllegalInstruction(0xF7, "ISC", As.ZeroPageXIndexed.Memory, Doing.Increment.Memory, Doing.SubtractWithCarry);
            _instructions[0xFA] = new Instruction(0xFA, "NOP*", As.Implied.Memory, Doing.Nop);
            _instructions[0xFB] = new IllegalInstruction(0xFB, "ISC", As.AbsoluteYIndexed.DoubleMemoryRead, Doing.Increment.Memory, Doing.SubtractWithCarry);
            _instructions[0xFC] = new Instruction(0xFC, "IGN", As.AbsoluteXIndexed.Common, Doing.Nop);
            _instructions[0xFF] = new IllegalInstruction(0xFF, "ISC", As.AbsoluteXIndexed.DoubleMemoryRead, Doing.Increment.Memory, Doing.SubtractWithCarry);
        }
    }
}