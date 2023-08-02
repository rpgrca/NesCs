using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    public class Builder
    {
        private ProcessorStatus _p;
        private byte _a, _x, _y, _s;
        private int _pc, _ramSize, _start, _end;
        private byte[] _program, _ram;
        private (int Address, byte Value)[] _patch;
        private readonly IInstruction[] _instructions;
        private List<(int, byte, string)> _trace;

        public Builder()
        {
            _p = ProcessorStatus.None;
            _a = _x = _y = _s = 0;
            _ramSize = _start = _end = _pc = 0;
            _program = _ram = Array.Empty<byte>();
            _patch = Array.Empty<(int, byte)>();
            _trace = new();

            _instructions = new IInstruction[0x100];
            for (var index = 0; index < 0x100; index++)
            {
                _instructions[index] = new NotImplementedInstruction(index);
            }

            // _instructions[0x00] W
            _instructions[0x01] = new OraInIndirectXModeOpcode01();
            //_instructions[0x02] R
            //_instructions[0x03] W
            //_instructions[0x04] R
            _instructions[0x05] = new OraInZeroPageModeOpcode05();
            _instructions[0x06] = new ShiftLeftZeroPageOpcode06();
            //_instructions[0x07] W
            _instructions[0x08] = new PushProcessorStatusOpcode08();
            _instructions[0x09] = new OraInImmediateModeOpcode09();
            _instructions[0x0A] = new ShiftLeftAccumulatorOpcode0A();
            //_instructions[0x0B] R
            //_instructions[0x0C] R
            _instructions[0x0D] = new OraInAbsoluteModeOpcode0D();
            _instructions[0x0E] = new ShiftLeftAbsoluteOpcode0E();
            //_instructions[0x0F] W
            _instructions[0x10] = new BranchIfPositiveOpcode10();
            _instructions[0x11] = new OraInIndirectYModeOpcode11();
            //_instructions[0x12] R
            //_instructions[0x13] W
            //_instructions[0x14] R
            _instructions[0x15] = new OraInZeroPageXModeOpcode15();
            _instructions[0x16] = new ShiftLeftZeroPageXOpcode16();
            //_instructions[0x17] W
            _instructions[0x18] = new ClearCarryFlagOpcode18();
            _instructions[0x19] = new OraInAbsoluteYModeOpcode19();
            //_instructions[0x1A] R
            //_instructions[0x1B] W
            //_instructions[0x1C] R
            _instructions[0x1D] = new OraInAbsoluteXModeOpcode0D();
            _instructions[0x1E] = new ShiftLeftAbsoluteXOpcode1E();
            //_instructions[0x1F] W
            _instructions[0x20] = new JumpToSubroutineOpcode20();
            _instructions[0x21] = new AndInIndirectXModeOpcode21();
            //_instructions[0x22] R
            //_instructions[0x23] W
            _instructions[0x24] = new BitTestZeroPageModeOpcode24();
            _instructions[0x25] = new AndInZeroPageModeOpcode25();
            _instructions[0x26] = new RotateLeftZeroPageOpcode26();
            //_instructions[0x27] W
            _instructions[0x28] = new PullProcessorStatusOpcode28();
            _instructions[0x29] = new AndInImmediateModeOpcode29();
            _instructions[0x2A] = new RotateLeftAccumulatorOpcode2A();
            //_instructions[0x2B] R
            _instructions[0x2C] = new BitTestAbsoluteOpcode2C();
            _instructions[0x2D] = new AndInAbsoluteModeOpcode2D();
            _instructions[0x2E] = new RotateLeftAbsoluteOpcode2E();
            //_instructions[0x2F] W
            _instructions[0x30] = new BranchIfMinusOpcode30();
            _instructions[0x31] = new AndInIndirectYModeOpcode31();
            //_instructions[0x32] R
            //_instructions[0x33] W
            //_instructions[0x34] R
            _instructions[0x35] = new AndInZeroPageXModeOpcode35();
            _instructions[0x36] = new RotateLeftZeroPageXOpcode36();
            //_instructions[0x37] W
            _instructions[0x38] = new SetCarryFlagOpcode38();
            _instructions[0x39] = new AndInAbsoluteYModeOpcode39();
            //_instructions[0x3A] R
            //_instructions[0x3B] W
            //_instructions[0x3C] R
            _instructions[0x3D] = new AndInAbsoluteXModeOpcode3D();
            _instructions[0x3E] = new RotateLeftAbsoluteXOpcode3E();
            //_instructions[0x3F] W
            _instructions[0x40] = new ReturnFromInterruptOpcode40();
            _instructions[0x41] = new XorInIndirectXModeOpcode41();
            //_instructions[0x42] R
            //_instructions[0x43] W
            //_instructions[0x44] R
            _instructions[0x45] = new XorInZeroPageModeOpcode45();
            _instructions[0x46] = new ShiftRightZeroPageOpcode46();
            //_instructions[0x47] W
            _instructions[0x48] = new PushAccumulatorOpcode48();
            _instructions[0x49] = new XorInImmediateModeOpcode49();
            _instructions[0x4A] = new ShiftRightAccumulatorOpcode4A();
            //_instructions[0x4B] R
            _instructions[0x4C] = new JumpInAbsoluteModeOpcode4C();
            _instructions[0x4D] = new XorInAbsoluteModeOpcode4D();
            _instructions[0x4E] = new ShiftRightAbsoluteOpcode4E();
            //_instructions[0x4F] W
            _instructions[0x50] = new BranchIfOverflowNotSetOpcode50();
            _instructions[0x51] = new XorInIndirectYModeOpcode51();
            //_instructions[0x52] R
            //_instructions[0x53] W
            //_instructions[0x54] R
            _instructions[0x55] = new XorInZeroPageXModeOpcode55();
            _instructions[0x56] = new ShiftRightZeroPageXOpcode56();
            //_instructions[0x57] W
            _instructions[0x58] = new ClearInterruptDisableOpcode58();
            _instructions[0x59] = new XorInAbsoluteYModeOpcode59();
            //_instructions[0x5A] R
            //_instructions[0x5B] W
            //_instructions[0x5C] R
            _instructions[0x5D] = new XorInAbsoluteXModeOpcode5D();
            _instructions[0x5E] = new ShiftRightAbsoluteXOpcode5E();
            //_instructions[0x5F] W
            _instructions[0x60] = new ReturnFromSubroutineOpcode60();
            _instructions[0x61] = new AddInIndirectXModeOpcode61();
            //_instructions[0x62] R
            //_instructions[0x63] W
            //_instructions[0x64] R
            _instructions[0x65] = new AddInZeroPageModeOpcode65();
            _instructions[0x66] = new RotateRightZeroPageOpcode66();
            //_instructions[0x67] W
            _instructions[0x68] = new PullAccumulatorOpcode68();
            _instructions[0x69] = new AddInImmediateModeOpcode69();
            _instructions[0x6A] = new RotateRightAccumulatorOpcode6A();
            //_instructions[0x6B] R
            _instructions[0x6C] = new JumpInIndirectModeOpcode6C();
            _instructions[0x6D] = new AddInAbsoluteModeOpcode6D();
            _instructions[0x6E] = new RotateRightAbsoluteOpcode6E();
            //_instructions[0x6F] W
            _instructions[0x70] = new BranchIfOverflowSetOpcode70();
            _instructions[0x71] = new AddInIndirectYModeOpcode71();
            //_instructions[0x72] R
            //_instructions[0x73] W
            //_instructions[0x74] R
            _instructions[0x75] = new AddInZeroPageXModeOpcode75();
            _instructions[0x76] = new RotateRightZeroPageXOpcode76();
            //_instructions[0x77] W
            _instructions[0x78] = new SetInterruptDisableOpcode78();
            _instructions[0x79] = new AddInAbsoluteYModeOpcode6D();
            //_instructions[0x7A] R
            //_instructions[0x7B] W
            //_instructions[0x7C] R
            _instructions[0x7D] = new AddInAbsoluteXModeOpcode6D();
            _instructions[0x7E] = new RotateRightAbsoluteXOpcode7E();
            //_instructions[0x7F] W
            //_instructions[0x80] R
            //_instructions[0x81] W
            //_instructions[0x82] R
            //_instructions[0x83] W
            //_instructions[0x84] W
            _instructions[0x85] = new StoreAccumulatorZeroPageOpcode85();
            //_instructions[0x86] W
            //_instructions[0x87] W
            _instructions[0x88] = new DecrementYOpcode88();
            //_instructions[0x89] R
            _instructions[0x8A] = new TransferXToAccumulatorOpcode8A();
            //_instructions[0x8B] R
            //_instructions[0x8C] W
            _instructions[0x8D] = new StoreAccumulatorAbsoluteOpcode8D();
            //_instructions[0x8E] W
            //_instructions[0x8F] W
            _instructions[0x90] = new BranchIfCarryNotSetOpcode90();
            //_instructions[0x91] W
            //_instructions[0x92] R
            //_instructions[0x93] W
            //_instructions[0x94] W
            _instructions[0x95] = new StoreAccumulatorZeroPageXOpcode95();
            //_instructions[0x96] W
            //_instructions[0x97] W
            _instructions[0x98] = new TransferYToAccumulatorOpcode98();
            //_instructions[0x99] W
            _instructions[0x9A] = new TransferXToStackOpcode9A();
            //_instructions[0x9B] W
            //_instructions[0x9C] W
            _instructions[0x9D] = new StoreAccumulatorAbsoluteXOpcode9D();
            //_instructions[0x9E] W
            //_instructions[0x9F] W
            _instructions[0xA0] = new LdyInImmediateModeOpcodeA0();
            _instructions[0xA1] = new LdaInIndirectXModeOpcodeA1();
            _instructions[0xA2] = new LdxInImmediateModeOpcodeA2();
            //_instructions[0xA3] R
            _instructions[0xA4] = new LdyInZeroPageModeOpcodeA4();
            _instructions[0xA5] = new LdaInZeroPageModeOpcodeA5();
            _instructions[0xA6] = new LdxInZeroPageModeOpcodeA6();
            //_instructions[0xA7] R
            _instructions[0xA8] = new TransferAccumulatorToYOpcodeA8();
            _instructions[0xA9] = new LdaInImmediateModeOpcodeA9();
            _instructions[0xAA] = new TransferAccumulatorToXOpcodeAA();
            //_instructions[0xAB] R
            _instructions[0xAC] = new LdyInAbsoluteModeOpcodeAC();
            _instructions[0xAD] = new LdaInAbsoluteModeOpcodeAD();
            _instructions[0xAE] = new LdxInAbsoluteModeOpcodeAE();
            //_instructions[0xAF] R
            _instructions[0xB0] = new BranchIfCarrySetOpcodeB0();
            _instructions[0xB1] = new LdaInIndirectYModeOpcodeB1();
            //_instructions[0xB2] R
            //_instructions[0xB3] R
            _instructions[0xB4] = new LdyInZeroPageXModeOpcodeB4();
            _instructions[0xB5] = new LdaInZeroPageXModeOpcodeB5();
            _instructions[0xB6] = new LdxInZeroPageYModeOpcodeB6();
            //_instructions[0xB7] R
            _instructions[0xB8] = new ClearOverflowFlagOpcodeB8();
            _instructions[0xB9] = new LdaInAbsoluteYModeOpcodeB9();
            _instructions[0xBA] = new TransferStackToXOpcodeBA();
            //_instructions[0xBB] R
            _instructions[0xBC] = new LdyInAbsoluteXModeOpcodeBC();
            _instructions[0xBD] = new LdaInAbsoluteXModeOpcodeBD();
            _instructions[0xBE] = new LdxInAbsoluteYModeOpcodeBE();
            //_instructions[0xBF] R
            _instructions[0xC0] = new CompareYImmediateModeOpcodeC0();
            _instructions[0xC1] = new CompareIndirectXModeOpcodeC1();
            //_instructions[0xC2] R
            //_instructions[0xC3] W
            _instructions[0xC4] = new CompareYZeroPageModeOpcodeC4();
            _instructions[0xC5] = new CompareZeroPageModeOpcodeC5();
            //_instructions[0xC6] W
            //_instructions[0xC7] W
            _instructions[0xC8] = new IncrementYOpcodeC8();
            _instructions[0xC9] = new CompareImmediateModeOpcodeC9();
            _instructions[0xCA] = new DecrementXOpcodeCA();
            //_instructions[0xCB] R
            _instructions[0xCC] = new CompareYAbsoluteModeOpcodeCC();
            _instructions[0xCD] = new CompareAbsoluteModeOpcodeCD();
            //_instructions[0xCE] W
            //_instructions[0xCF] W
            _instructions[0xD0] = new BranchIfNotEqualOpcodeD0();
            _instructions[0xD1] = new CompareIndirectYModeOpcodeD1();
            //_instructions[0xD2] R
            //_instructions[0xD3] W
            //_instructions[0xD4] R
            _instructions[0xD5] = new CompareZeroPageXModeOpcodeD5();
            //_instructions[0xD6] W
            //_instructions[0xD7] W
            _instructions[0xD8] = new ClearDecimalModeOpcodeD8();
            _instructions[0xD9] = new CompareAbsoluteYModeOpcodeD9();
            //_instructions[0xDA] R
            //_instructions[0xDB] W
            //_instructions[0xDC] R
            _instructions[0xDD] = new CompareAbsoluteXModeOpcodeDD();
            //_instructions[0xDE] W
            //_instructions[0xDF] W
            _instructions[0xE0] = new CompareXImmediateModeOpcodeE0();
            _instructions[0xE1] = new SubtractInIndirectXModeOpcodeE1();
            //_instructions[0xE2] R
            //_instructions[0xE3] W
            _instructions[0xE4] = new CompareXZeroPageModeOpcodeE4();
            _instructions[0xE5] = new SubtractInZeroPageModeOpcodeE5();
            //_instructions[0xE6] W
            //_instructions[0xE7] W
            _instructions[0xE8] = new IncrementXOpcodeE8();
            _instructions[0xE9] = new SubtractInImmediateModeOpcodeE9();
            _instructions[0xEA] = new NopOpcodeEA();
            //_instructions[0xEB] R
            _instructions[0xEC] = new CompareXAbsoluteModeOpcodeEC();
            _instructions[0xED] = new SubtractInAbsoluteModeOpcodeED();
            //_instructions[0xEE] W
            //_instructions[0xEF] W
            _instructions[0xF0] = new BranchIfEqualOpcodeF0();
            _instructions[0xF1] = new SubtractInIndirectYModeOpcodeF1();
            //_instructions[0xF2] R
            //_instructions[0xF3] W
            //_instructions[0xF4] R
            _instructions[0xF5] = new SubtractInZeroPageXModeOpcodeF5();
            //_instructions[0xF6] W
            //_instructions[0xF7] W
            _instructions[0xF8] = new SetDecimalFlagOpcodeF8();
            _instructions[0xF9] = new SubtractInAbsoluteYModeOpcodeF9();
            //_instructions[0xFA] R
            //_instructions[0xFB] W
            //_instructions[0xFC] R
            _instructions[0xFD] = new SubtractInAbsoluteXModeOpcodeFD();
            //_instructions[0xFE] W
            //_instructions[0xFF] W
        }

        public Builder Running(byte[] program)
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

        public Builder TracingWith(List<(int, byte, string)> trace)
        {
            _trace = trace;
            return this;
        }

        public Cpu6502 Build()
        {
            _patch ??= Array.Empty<(int, byte)>();
            if (_ramSize < 1) _ramSize = 0x10000;
            if (_ram.Length < 1) _ram = new byte[_ramSize];
            if (_end < 1) _end = _program.Length;

            foreach ((int address, byte value) in _patch)
            {
                _ram[address] = value;
            }

            return new Cpu6502(_program, _start, _end, _pc, _a, _x, _y, _s, _p, _ram, _instructions, _trace);
        }
    }
}