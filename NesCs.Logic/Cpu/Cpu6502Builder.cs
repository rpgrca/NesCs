using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    public class Builder
    {
        private ProcessorStatus _p;
        private byte _a, _x, _y, _s;
        private int _pc, _ramSize, _programSize, _cycles, _nmiVector, _resetVector, _irqVector;
        private byte[] _program;
        private readonly List<int> _mappedProgramAddresses;
        private (int Address, byte Value)[] _patch;
        private readonly IInstruction[] _instructions;
        private readonly Dictionary<int, Action<Cpu6502>> _callbacks;
        private ITracer _tracer;
        private bool _enableInvalid;
        private readonly Addressings.Addressings A;
        private readonly Operations.Operations Doing;

        public Builder()
        {
            _enableInvalid = false;
            _nmiVector = 0xFFFA;
            _resetVector = 0xFFFC;
            _irqVector = 0xFFFE;
            _mappedProgramAddresses = new List<int>();
            _callbacks = new Dictionary<int, Action<Cpu6502>>();
            _p = ProcessorStatus.None;
            _a = _x = _y = _s = 0;
            _ramSize = _pc = _programSize = _cycles = 0;
            _program = Array.Empty<byte>();
            _patch = Array.Empty<(int, byte)>();
            _tracer = new DummyTracer();
            A = new Addressings.Addressings();
            Doing = new Operations.Operations();

            _instructions = new IInstruction[0x100];
            for (var index = 0; index < 0x100; index++)
            {
                _instructions[index] = new NotImplementedInstruction(index);
            }

            _instructions[0x00] = new ForceInterruptOpcode00();
            _instructions[0x01] = new Instruction(0x01, "ORA", A.IndirectXIndexed, Doing.Or);
            _instructions[0x05] = new Instruction(0x05, "ORA", A.ZeroPage, Doing.Or);
            _instructions[0x06] = new Instruction(0x06, "ASL", A.ZeroPage, Doing.ShiftLeft);
            _instructions[0x08] = new PushProcessorStatusOpcode08();
            _instructions[0x09] = new Instruction(0x09, "ORA", A.Immediate, Doing.Or);
            _instructions[0x0A] = new ShiftLeftAccumulatorOpcode0A();
            _instructions[0x0D] = new Instruction(0x0D, "ORA", A.Absolute, Doing.Or);
            _instructions[0x0E] = new Instruction(0x0E, "ASL", A.Absolute, Doing.ShiftLeft);
            _instructions[0x10] = new BranchIfPositiveOpcode10();
            _instructions[0x11] = new Instruction(0x11, "ORA", A.IndirectYIndexed, Doing.Or);
            _instructions[0x15] = new Instruction(0x15, "ORA", A.ZeroPageXIndexed, Doing.Or);
            _instructions[0x16] = new Instruction(0x16, "ASL", A.ZeroPageXIndexed, Doing.ShiftLeft);
            _instructions[0x18] = new Instruction(0x18, "CLC", A.Implied, Doing.Flag.Minus.C);
            _instructions[0x19] = new Instruction(0x19, "ORA", A.AbsoluteYIndexed, Doing.Or);
            _instructions[0x1D] = new Instruction(0x1D, "ORA", A.AbsoluteXIndexed, Doing.Or);
            _instructions[0x1E] = new ShiftLeftAbsoluteXOpcode1E();
            _instructions[0x20] = new JumpToSubroutineOpcode20();
            _instructions[0x21] = new Instruction(0x21, "AND", A.IndirectXIndexed, Doing.And);
            _instructions[0x24] = new Instruction(0x24, "BIT", A.ZeroPage, Doing.BitTest);
            _instructions[0x25] = new Instruction(0x25, "AND", A.ZeroPage, Doing.And);
            _instructions[0x26] = new RotateLeftZeroPageOpcode26();
            _instructions[0x28] = new PullProcessorStatusOpcode28();
            _instructions[0x29] = new Instruction(0x29, "AND", A.Immediate, Doing.And);
            _instructions[0x2A] = new RotateLeftAccumulatorOpcode2A();
            _instructions[0x2C] = new Instruction(0x2C, "BIT", A.Absolute, Doing.BitTest);
            _instructions[0x2D] = new Instruction(0x2D, "AND", A.Absolute, Doing.And);
            _instructions[0x2E] = new RotateLeftAbsoluteOpcode2E();
            _instructions[0x30] = new BranchIfMinusOpcode30();
            _instructions[0x31] = new Instruction(0x31, "AND", A.IndirectYIndexed, Doing.And);
            _instructions[0x35] = new Instruction(0x35, "AND", A.ZeroPageXIndexed, Doing.And);
            _instructions[0x36] = new RotateLeftZeroPageXOpcode36();
            _instructions[0x38] = new Instruction(0x38, "SEC", A.Implied, Doing.Flag.Plus.C);
            _instructions[0x39] = new Instruction(0x39, "AND", A.AbsoluteYIndexed, Doing.And);
            _instructions[0x3D] = new Instruction(0x3D, "AND", A.AbsoluteXIndexed, Doing.And);
            _instructions[0x3E] = new RotateLeftAbsoluteXOpcode3E();
            _instructions[0x40] = new ReturnFromInterruptOpcode40();
            _instructions[0x41] = new XorInIndirectXModeOpcode41();
            _instructions[0x45] = new XorInZeroPageModeOpcode45();
            _instructions[0x46] = new ShiftRightZeroPageOpcode46();
            _instructions[0x48] = new PushAccumulatorOpcode48();
            _instructions[0x49] = new XorInImmediateModeOpcode49();
            _instructions[0x4A] = new ShiftRightAccumulatorOpcode4A();
            _instructions[0x4C] = new JumpInAbsoluteModeOpcode4C();
            _instructions[0x4D] = new XorInAbsoluteModeOpcode4D();
            _instructions[0x4E] = new ShiftRightAbsoluteOpcode4E();
            _instructions[0x50] = new BranchIfOverflowNotSetOpcode50();
            _instructions[0x51] = new XorInIndirectYModeOpcode51();
            _instructions[0x55] = new XorInZeroPageXModeOpcode55();
            _instructions[0x56] = new ShiftRightZeroPageXOpcode56();
            _instructions[0x58] = new Instruction(0x58, "CLI", A.Implied, Doing.Flag.Minus.I);
            _instructions[0x59] = new XorInAbsoluteYModeOpcode59();
            _instructions[0x5D] = new XorInAbsoluteXModeOpcode5D();
            _instructions[0x5E] = new ShiftRightAbsoluteXOpcode5E();
            _instructions[0x60] = new ReturnFromSubroutineOpcode60();
            _instructions[0x61] = new Instruction(0x61, "ADC", A.IndirectXIndexed, Doing.AddWithCarry);
            _instructions[0x65] = new Instruction(0x65, "ADC", A.ZeroPage, Doing.AddWithCarry);
            _instructions[0x66] = new RotateRightZeroPageOpcode66();
            _instructions[0x68] = new PullAccumulatorOpcode68();
            _instructions[0x69] = new Instruction(0x69, "ADC", A.Immediate, Doing.AddWithCarry);
            _instructions[0x6A] = new RotateRightAccumulatorOpcode6A();
            _instructions[0x6C] = new JumpInIndirectModeOpcode6C();
            _instructions[0x6D] = new Instruction(0x6D, "ADC", A.Absolute, Doing.AddWithCarry);
            _instructions[0x6E] = new RotateRightAbsoluteOpcode6E();
            _instructions[0x70] = new BranchIfOverflowSetOpcode70();
            _instructions[0x71] = new Instruction(0x71, "ADC", A.IndirectYIndexed, Doing.AddWithCarry);
            _instructions[0x75] = new Instruction(0x75, "ADC", A.ZeroPageXIndexed, Doing.AddWithCarry);
            _instructions[0x76] = new RotateRightZeroPageXOpcode76();
            _instructions[0x78] = new Instruction(0x78, "SEI", A.Implied, Doing.Flag.Plus.I);
            _instructions[0x79] = new Instruction(0x79, "ADC", A.AbsoluteYIndexed, Doing.AddWithCarry);
            _instructions[0x7D] = new Instruction(0x7D, "ADC", A.AbsoluteXIndexed, Doing.AddWithCarry);
            _instructions[0x7E] = new RotateRightAbsoluteXOpcode7E();
            _instructions[0x81] = new StoreAccumulatorIndirectXOpcode81();
            _instructions[0x84] = new StoreRegisterYZeroPageOpcode84();
            _instructions[0x85] = new StoreAccumulatorZeroPageOpcode85();
            _instructions[0x86] = new StoreRegisterXZeroPageOpcode86();
            _instructions[0x88] = new DecrementYOpcode88();
            _instructions[0x8A] = new TransferXToAccumulatorOpcode8A();
            _instructions[0x8C] = new StoreRegisterYAbsoluteOpcode8C();
            _instructions[0x8D] = new StoreAccumulatorAbsoluteOpcode8D();
            _instructions[0x8E] = new StoreRegisterXAbsoluteOpcode8E();
            _instructions[0x90] = new BranchIfCarryNotSetOpcode90();
            _instructions[0x91] = new StoreAccumulatorIndirectYOpcode91();
            _instructions[0x94] = new StoreRegisterYZeroPageXOpcode94();
            _instructions[0x95] = new StoreAccumulatorZeroPageXOpcode95();
            _instructions[0x96] = new StoreRegisterXZeroPageYOpcode96();
            _instructions[0x98] = new TransferYToAccumulatorOpcode98();
            _instructions[0x99] = new StoreAccumulatorAbsoluteYOpcode99();
            _instructions[0x9A] = new TransferXToStackOpcode9A();
            _instructions[0x9D] = new StoreAccumulatorAbsoluteXOpcode9D();
            _instructions[0xA0] = new LdyInImmediateModeOpcodeA0();
            _instructions[0xA1] = new LdaInIndirectXModeOpcodeA1();
            _instructions[0xA2] = new LdxInImmediateModeOpcodeA2();
            _instructions[0xA4] = new LdyInZeroPageModeOpcodeA4();
            _instructions[0xA5] = new LdaInZeroPageModeOpcodeA5();
            _instructions[0xA6] = new LdxInZeroPageModeOpcodeA6();
            _instructions[0xA8] = new TransferAccumulatorToYOpcodeA8();
            _instructions[0xA9] = new LdaInImmediateModeOpcodeA9();
            _instructions[0xAA] = new TransferAccumulatorToXOpcodeAA();
            _instructions[0xAC] = new LdyInAbsoluteModeOpcodeAC();
            _instructions[0xAD] = new LdaInAbsoluteModeOpcodeAD();
            _instructions[0xAE] = new LdxInAbsoluteModeOpcodeAE();
            _instructions[0xB0] = new BranchIfCarrySetOpcodeB0();
            _instructions[0xB1] = new LdaInIndirectYModeOpcodeB1();
            _instructions[0xB4] = new LdyInZeroPageXModeOpcodeB4();
            _instructions[0xB5] = new LdaInZeroPageXModeOpcodeB5();
            _instructions[0xB6] = new LdxInZeroPageYModeOpcodeB6();
            _instructions[0xB8] = new Instruction(0xB8, "CLV", A.Implied, Doing.Flag.Minus.V);
            _instructions[0xB9] = new LdaInAbsoluteYModeOpcodeB9();
            _instructions[0xBA] = new TransferStackToXOpcodeBA();
            _instructions[0xBC] = new LdyInAbsoluteXModeOpcodeBC();
            _instructions[0xBD] = new LdaInAbsoluteXModeOpcodeBD();
            _instructions[0xBE] = new LdxInAbsoluteYModeOpcodeBE();
            _instructions[0xC0] = new Instruction(0xC0, "CMP", A.Immediate, Doing.Compare.Y);
            _instructions[0xC1] = new Instruction(0xC1, "CMP", A.IndirectXIndexed, Doing.Compare.Accumulator);
            _instructions[0xC4] = new Instruction(0xC4, "CMP", A.ZeroPage, Doing.Compare.Y);
            _instructions[0xC5] = new Instruction(0xC5, "CMP", A.ZeroPage, Doing.Compare.Accumulator);
            _instructions[0xC6] = new DecrementMemoryZeroPageOpcodeC6();
            _instructions[0xC8] = new IncrementYOpcodeC8();
            _instructions[0xC9] = new Instruction(0xC9, "CMP", A.Immediate, Doing.Compare.Accumulator);
            _instructions[0xCA] = new DecrementXOpcodeCA();
            _instructions[0xCC] = new Instruction(0xCC, "CMP", A.Absolute, Doing.Compare.Y);
            _instructions[0xCD] = new Instruction(0xCD, "CMP", A.Absolute, Doing.Compare.Accumulator);
            _instructions[0xCE] = new DecrementMemoryAbsoluteOpcodeCE();
            _instructions[0xD0] = new BranchIfNotEqualOpcodeD0();
            _instructions[0xD1] = new Instruction(0xD1, "CMP", A.IndirectYIndexed, Doing.Compare.Accumulator);
            _instructions[0xD5] = new Instruction(0xD5, "CMP", A.ZeroPageXIndexed, Doing.Compare.Accumulator);
            _instructions[0xD6] = new DecrementMemoryZeroPageXOpcodeD6();
            _instructions[0xD8] = new Instruction(0xD8, "CLD", A.Implied, Doing.Flag.Minus.D);
            _instructions[0xD9] = new Instruction(0xD9, "CMP", A.AbsoluteYIndexed, Doing.Compare.Accumulator);
            _instructions[0xDD] = new Instruction(0xDD, "CMP", A.AbsoluteXIndexed, Doing.Compare.Accumulator);
            _instructions[0xDE] = new DecrementMemoryAbsoluteXOpcodeDE();
            _instructions[0xE0] = new Instruction(0xE0, "CMP", A.Immediate, Doing.Compare.X);
            _instructions[0xE1] = new Instruction(0xE1, "SBC", A.IndirectXIndexed, Doing.SubtractWithCarry);
            _instructions[0xE4] = new Instruction(0xE4, "CMP", A.ZeroPage, Doing.Compare.X);
            _instructions[0xE5] = new Instruction(0xE5, "SBC", A.ZeroPage, Doing.SubtractWithCarry);
            _instructions[0xE6] = new IncrementMemoryZeroPageOpcodeE6();
            _instructions[0xE8] = new IncrementXOpcodeE8();
            _instructions[0xE9] = new Instruction(0xE9, "SBC", A.Immediate, Doing.SubtractWithCarry);
            _instructions[0xEA] = new Instruction(0xEA, "NOP", A.Implied, Doing.Nop);
            _instructions[0xEC] = new Instruction(0xEC, "CMP", A.Absolute, Doing.Compare.X);
            _instructions[0xED] = new Instruction(0xED, "SBC", A.Absolute, Doing.SubtractWithCarry);
            _instructions[0xEE] = new IncrementMemoryAbsoluteOpcodeEE();
            _instructions[0xF0] = new BranchIfEqualOpcodeF0();
            _instructions[0xF1] = new Instruction(0xF1, "SBC", A.IndirectYIndexed, Doing.SubtractWithCarry);
            _instructions[0xF5] = new Instruction(0xF5, "SBC", A.ZeroPageXIndexed, Doing.SubtractWithCarry);
            _instructions[0xF6] = new IncrementMemoryZeroPageXOpcodeF6();
            _instructions[0xF8] = new Instruction(0xF8, "SED", A.Implied, Doing.Flag.Plus.D);
            _instructions[0xF9] = new Instruction(0xF9, "SBC", A.AbsoluteYIndexed, Doing.SubtractWithCarry);
            _instructions[0xFD] = new Instruction(0xFD, "SBC", A.AbsoluteXIndexed, Doing.SubtractWithCarry);
            _instructions[0xFE] = new IncrementMemoryAbsoluteXOpcodeFE();
        }

        public Builder Running(byte[] program)
        {
            _program = program;
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

        public Builder WithCyclesAs(int cycles)
        {
            _cycles = cycles;
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

        public Builder WithCallback(int address, Action<Cpu6502> callback)
        {
            _callbacks.Add(address, callback);
            return this;
        }

        public Cpu6502 Build()
        {
            _patch ??= Array.Empty<(int, byte)>();
            if (_programSize < 1) _programSize = _program.Length;
            if (_ramSize < 1) _ramSize = 0x10000;
            if (_enableInvalid) AddInvalidOpcodes();

            return new Cpu6502(_program, _programSize, _ramSize, _mappedProgramAddresses.ToArray(),
                _pc, _a, _x, _y, _s, _p, _cycles, _patch, _instructions, _tracer, _callbacks,
                _resetVector, _nmiVector, _irqVector);
        }

        private void AddInvalidOpcodes()
        {
            //_instructions[0x02] R
            _instructions[0x03] = new IllegalShiftLeftOrOpcode03();
            _instructions[0x04] = new IllegalReadIgnoreOpcode04();
            _instructions[0x07] = new IllegalShiftLeftOrOpcode07();
            //_instructions[0x0B] R
            _instructions[0x0C] = new IllegalReadIgnoreOpcode0C();
            _instructions[0x0F] = new IllegalShiftLeftOrOpcode0F();
            //_instructions[0x12] R
            _instructions[0x13] = new IllegalShiftLeftOrOpcode13();
            _instructions[0x14] = new IllegalReadIgnoreOpcode14();
            _instructions[0x17] = new IllegalShiftLeftOrOpcode17();
            _instructions[0x1A] = new Instruction(0x1A, "NOP*", A.Implied, Doing.Nop);
            _instructions[0x1B] = new IllegalShiftLeftOrOpcode1B();
            _instructions[0x1C] = new IllegalReadIgnoreOpcode1C();
            _instructions[0x1F] = new IllegalShiftLeftOrOpcode1F();
            //_instructions[0x22] R
            _instructions[0x23] = new IllegalRotateLeftAndOpcode23();
            _instructions[0x27] = new IllegalRotateLeftAndOpcode27();
            //_instructions[0x2B] R
            _instructions[0x2F] = new IllegalRotateLeftAndOpcode2F();
            //_instructions[0x32] R
            _instructions[0x33] = new IllegalRotateLeftAndOpcode33();
            _instructions[0x34] = new IllegalReadIgnoreOpcode34();
            _instructions[0x37] = new IllegalRotateLeftAndOpcode37();
            _instructions[0x3A] = new Instruction(0x3A, "NOP*", A.Implied, Doing.Nop);
            _instructions[0x3B] = new IllegalRotateLeftAndOpcode3B();
            _instructions[0x3C] = new IllegalReadIgnoreOpcode3C();
            _instructions[0x3F] = new IllegalRotateLeftAndOpcode3F();
            //_instructions[0x42] R
            _instructions[0x43] = new IllegalShiftRightXorOpcode43();
            _instructions[0x44] = new IllegalReadIgnoreOpcode44();
            _instructions[0x47] = new IllegalShiftRightXorOpcode47();
            //_instructions[0x4B] R
            _instructions[0x4F] = new IllegalShiftRightXorOpcode4F();
            //_instructions[0x52] R
            _instructions[0x53] = new IllegalShiftRightXorOpcode53();
            _instructions[0x54] = new IllegalReadIgnoreOpcode54();
            _instructions[0x57] = new IllegalShiftRightXorOpcode57();
            _instructions[0x5A] = new Instruction(0x5A, "NOP*", A.Implied, Doing.Nop);
            _instructions[0x5B] = new IllegalShiftRightXorOpcode5B();
            _instructions[0x5C] = new IllegalReadIgnoreOpcode5C();
            _instructions[0x5F] = new IllegalShiftRightXorOpcode5F();
            //_instructions[0x62] R
            _instructions[0x63] = new IllegalRotateRightAddOpcode63();
            _instructions[0x64] = new IllegalReadIgnoreOpcode64();
            _instructions[0x67] = new IllegalRotateRightAddOpcode67();
            //_instructions[0x6B] R
            _instructions[0x6F] = new IllegalRotateRightAddOpcode6F();
            //_instructions[0x72] R
            _instructions[0x73] = new IllegalRotateRightAddOpcode73();
            _instructions[0x74] = new IllegalReadIgnoreOpcode74();
            _instructions[0x77] = new IllegalRotateRightAddOpcode77();
            _instructions[0x7A] = new Instruction(0x7A, "NOP*", A.Implied, Doing.Nop);
            _instructions[0x7B] = new IllegalRotateRightAddOpcode7B();
            _instructions[0x7C] = new IllegalReadIgnoreOpcode7C();
            _instructions[0x7F] = new IllegalRotateRightAddOpcode7F();
            _instructions[0x80] = new IllegalReadSkipOpcode80();
            _instructions[0x82] = new IllegalReadSkipOpcode82();
            _instructions[0x83] = new IllegalSaxIndirectXOpcode83();
            _instructions[0x87] = new IllegalSaxAbsoluteYOpcode87();
            _instructions[0x89] = new IllegalReadSkipOpcode89();
            //_instructions[0x8B] R XAA, ANE
            _instructions[0x8F] = new IllegalSaxAbsoluteOpcode8F();
            //_instructions[0x92] R
            //_instructions[0x93] W
            _instructions[0x97] = new IllegalSaxIndirectYOpcode97();
            //_instructions[0x9B] W
            //_instructions[0x9C] W
            //_instructions[0x9E] W
            //_instructions[0x9F] W
            _instructions[0xA3] = new IllegalLaxIndirectYOpcodeA3();
            _instructions[0xA7] = new IllegalLaxIndirectOpcodeA7();
            //_instructions[0xAB] R
            _instructions[0xAF] = new IllegalLaxAbsoluteOpcodeAF();
            //_instructions[0xB2] R
            _instructions[0xB3] = new IllegalLaxIndirectYOpcodeB3();
            _instructions[0xB7] = new IllegalLaxDirectYOpcodeB7();
            //_instructions[0xBB] R
            _instructions[0xBF] = new IllegalLaxAbsoluteYOpcodeBF();
            _instructions[0xC2] = new IllegalReadSkipOpcodeC2();
            _instructions[0xC3] = new IllegalDecrementCompareOpcodeC3();
            _instructions[0xC7] = new IllegalDecrementCompareOpcodeC7();
            //_instructions[0xCB] R AXS / SBX
            _instructions[0xCF] = new IllegalDecrementCompareOpcodeCF();
            //_instructions[0xD2] R
            _instructions[0xD3] = new IllegalDecrementCompareOpcodeD3();
            _instructions[0xD4] = new IllegalReadIgnoreOpcodeD4();
            _instructions[0xD7] = new IllegalDecrementCompareOpcodeD7();
            _instructions[0xDA] = new Instruction(0xDA, "NOP*", A.Implied, Doing.Nop);
            _instructions[0xDB] = new IllegalDecrementCompareOpcodeDB();
            _instructions[0xDC] = new IllegalReadIgnoreOpcodeDC();
            _instructions[0xDF] = new IllegalDecrementCompareOpcodeDF();
            _instructions[0xE2] = new IllegalReadSkipOpcodeE2();
            _instructions[0xE3] = new IllegalIncrementSubtractOpcodeE3();
            _instructions[0xE7] = new IllegalIncrementSubtractOpcodeE7();
            _instructions[0xEB] = new IllegalSubtractOpcodeEB();
            _instructions[0xEF] = new IllegalIncrementSubtractOpcodeEF();
            //_instructions[0xF2] R
            _instructions[0xF3] = new IllegalIncrementSubtractOpcodeF3();
            _instructions[0xF4] = new IllegalReadIgnoreOpcodeF4();
            _instructions[0xF7] = new IllegalIncrementSubtractOpcodeF7();
            _instructions[0xFA] = new Instruction(0xFA, "NOP*", A.Implied, Doing.Nop);
            _instructions[0xFB] = new IllegalIncrementSubtractOpcodeFB();
            _instructions[0xFC] = new IllegalReadIgnoreOpcodeFC();
            _instructions[0xFF] = new IllegalIncrementSubtractOpcodeFF();
        }
    }
}