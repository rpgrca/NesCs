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
                _instructions[index] = new NotImplementedInstruction();
            }

            _instructions[0x01] = new OraInIndirectXModeOpcode01();
            //_instructions[0x02] 
            //_instructions[0x04]
            _instructions[0x05] = new OraInZeroPageModeOpcode05();
            _instructions[0x09] = new OraInImmediateModeOpcode09();
            _instructions[0x0A] = new ShiftLeftAccumulatorOpcode0A();
            //_instructions[0x0B]
            //_instructions[0x0C]
            _instructions[0x0D] = new OraInAbsoluteModeOpcode0D();
            _instructions[0x10] = new BranchIfPositiveOpcode10();
            _instructions[0x11] = new OraInIndirectYModeOpcode11();
            //_instructions[0x12]
            //_instructions[0x14]
            _instructions[0x15] = new OraInZeroPageXModeOpcode15();
            _instructions[0x18] = new ClearCarryFlagOpcode18();
            _instructions[0x19] = new OraInAbsoluteYModeOpcode19();
            //_instructions[0x1A]
            //_instructions[0x1C]
            _instructions[0x1D] = new OraInAbsoluteXModeOpcode0D();
            _instructions[0x21] = new AndInIndirectXModeOpcode21();
            //_instructions[0x22]
            _instructions[0x24] = new BitTestZeroPageModeOpcode24();
            _instructions[0x25] = new AndInZeroPageModeOpcode25();
            _instructions[0x28] = new PullProcessorStatusOpcode28();
            _instructions[0x29] = new AndInImmediateModeOpcode29();
            _instructions[0x2A] = new RotateLeftAccumulatorOpcode2A();
            //_instructions[0x2B]
            _instructions[0x2C] = new BitTestAbsoluteOpcode2C();
            _instructions[0x2D] = new AndInAbsoluteModeOpcode2D();
            _instructions[0x30] = new BranchIfMinusOpcode30();
            _instructions[0x31] = new AndInIndirectYModeOpcode31();
            //_instructions[0x32]
            //_instructions[0x34]
            _instructions[0x35] = new AndInZeroPageXModeOpcode35();
            _instructions[0x38] = new SetCarryFlagOpcode38();
            _instructions[0x39] = new AndInAbsoluteYModeOpcode39();
            //_instructions[0x3A]
            //_instructions[0x3C]
            _instructions[0x3D] = new AndInAbsoluteXModeOpcode3D();
            _instructions[0x40] = new ReturnFromInterruptOpcode40();
            _instructions[0x41] = new XorInIndirectXModeOpcode41();
            //_instructions[0x42]
            //_instructions[0x44]
            _instructions[0x45] = new XorInZeroPageModeOpcode45();
            _instructions[0x49] = new XorInImmediateModeOpcode49();
            _instructions[0x4A] = new ShiftRightAccumulatorOpcode4A();
            //_instructions[0x4B]
            _instructions[0x4C] = new JumpInAbsoluteModeOpcode4C();
            _instructions[0x4D] = new XorInAbsoluteModeOpcode4D();
            _instructions[0x50] = new BranchIfOverflowNotSetOpcode50();
            _instructions[0x51] = new XorInIndirectYModeOpcode51();
            //_instructions[0x52]
            //_instructions[0x54]
            _instructions[0x55] = new XorInZeroPageXModeOpcode55();
            _instructions[0x58] = new ClearInterruptDisableOpcode58();
            _instructions[0x59] = new XorInAbsoluteYModeOpcode59();
            //_instructions[0x5A]
            //_instructions[0x5C]
            _instructions[0x5D] = new XorInAbsoluteXModeOpcode5D();
            _instructions[0x60] = new ReturnFromSubroutineOpcode60();
            _instructions[0x61] = new AddInIndirectXModeOpcode61();
            //_instructions[0x62]
            //_instructions[0x64]
            _instructions[0x65] = new AddInZeroPageModeOpcode65();
            //_instructions[0x68]
            _instructions[0x69] = new AddInImmediateModeOpcode69();
            //_instructions[0x6A]
            //_instructions[0x6B]
            _instructions[0x6C] = new JumpInIndirectModeOpcode6C();
            _instructions[0x6D] = new AddInAbsoluteModeOpcode6D();
            _instructions[0x70] = new BranchIfOverflowSetOpcode70();
            _instructions[0x71] = new AddInIndirectYModeOpcode71();
            //_instructions[0x72]
            //_instructions[0x74]
            _instructions[0x75] = new AddInZeroPageXModeOpcode75();
            _instructions[0x78] = new SetInterruptDisableOpcode78();
            _instructions[0x79] = new AddInAbsoluteYModeOpcode6D();
            //_instructions[0x7A]
            //_instructions[0x7C]
            _instructions[0x7D] = new AddInAbsoluteXModeOpcode6D();
            //_instructions[0x80]
            //_instructions[0x82]
            _instructions[0x88] = new DecrementYOpcode88();
            //_instructions[0x89]
            _instructions[0x8A] = new TransferXToAccumulatorOpcode8A();
            //_instructions[0x8B]
            _instructions[0x90] = new BranchIfCarryNotSetOpcode90();
            //_instructions[0x92]
            _instructions[0x98] = new TransferYToAccumulatorOpcode98();
            _instructions[0x9A] = new TransferXToStackOpcode9A();
            _instructions[0xA0] = new LdyInImmediateModeOpcodeA0();
            _instructions[0xA1] = new LdaInIndirectXModeOpcodeA1();
            _instructions[0xA2] = new LdxInImmediateModeOpcodeA2();
            //_instructions[0xA3]
            _instructions[0xA4] = new LdyInZeroPageModeOpcodeA4();
            _instructions[0xA5] = new LdaInZeroPageModeOpcodeA5();
            _instructions[0xA6] = new LdxInZeroPageModeOpcodeA6();
            //_instructions[0xA7]
            _instructions[0xA8] = new TransferAccumulatorToYOpcodeA8();
            _instructions[0xA9] = new LdaInImmediateModeOpcodeA9();
            _instructions[0xAA] = new TransferAccumulatorToXOpcodeAA();
            //_instructions[0xAB]
            _instructions[0xAC] = new LdyInAbsoluteModeOpcodeAC();
            _instructions[0xAD] = new LdaInAbsoluteModeOpcodeAD();
            _instructions[0xAE] = new LdxInAbsoluteModeOpcodeAE();
            //_instructions[0xAF]
            _instructions[0xB0] = new BranchIfCarrySetOpcodeB0();
            _instructions[0xB1] = new LdaInIndirectYModeOpcodeB1();
            //_instructions[0xB2]
            //_instructions[0xB3]
            _instructions[0xB4] = new LdyInZeroPageXModeOpcodeB4();
            _instructions[0xB5] = new LdaInZeroPageXModeOpcodeB5();
            _instructions[0xB6] = new LdxInZeroPageYModeOpcodeB6();
            //_instructions[0xB7]
            _instructions[0xB8] = new ClearOverflowFlagOpcodeB8();
            _instructions[0xB9] = new LdaInAbsoluteYModeOpcodeB9();
            _instructions[0xBA] = new TransferStackToXOpcodeBA();
            //_instructions[0xBB]
            _instructions[0xBC] = new LdyInAbsoluteXModeOpcodeBC();
            _instructions[0xBD] = new LdaInAbsoluteXModeOpcodeBD();
            _instructions[0xBE] = new LdxInAbsoluteYModeOpcodeBE();
            //_instructions[0xBF]
            _instructions[0xC0] = new CompareYImmediateModeOpcodeC0();
            _instructions[0xC1] = new CompareIndirectXModeOpcodeC1();
            //_instructions[0xC2]
            _instructions[0xC4] = new CompareYZeroPageModeOpcodeC4();
            _instructions[0xC5] = new CompareZeroPageModeOpcodeC5();
            _instructions[0xC8] = new IncrementYOpcodeC8();
            _instructions[0xC9] = new CompareImmediateModeOpcodeC9();
            _instructions[0xCA] = new DecrementXOpcodeCA();
            //_instructions[0xCB]
            _instructions[0xCC] = new CompareYAbsoluteModeOpcodeCC();
            _instructions[0xCD] = new CompareAbsoluteModeOpcodeCD();
            _instructions[0xD0] = new BranchIfNotEqualOpcodeD0();
            _instructions[0xD1] = new CompareIndirectYModeOpcodeD1();
            //_instructions[0xD2]
            //_instructions[0xD4]
            _instructions[0xD5] = new CompareZeroPageXModeOpcodeD5();
            _instructions[0xD8] = new ClearDecimalModeOpcodeD8();
            _instructions[0xD9] = new CompareAbsoluteYModeOpcodeD9();
            //_instructions[0xDA]
            //_instructions[0xDC]
            _instructions[0xDD] = new CompareAbsoluteXModeOpcodeDD();
            _instructions[0xE0] = new CompareXImmediateModeOpcodeE0();
            _instructions[0xE1] = new SubtractInIndirectXModeOpcodeE1();
            //_instructions[0xE2] = 
            _instructions[0xE4] = new CompareXZeroPageModeOpcodeE4();
            _instructions[0xE5] = new SubtractInZeroPageModeOpcodeE5();
            _instructions[0xE8] = new IncrementXOpcodeE8();
            _instructions[0xE9] = new SubtractInImmediateModeOpcodeE9();
            _instructions[0xEA] = new NopOpcodeEA();
            //_instructions[0xEB] =
            _instructions[0xEC] = new CompareXAbsoluteModeOpcodeEC();
            _instructions[0xED] = new SubtractInAbsoluteModeOpcodeED();
            _instructions[0xF0] = new BranchIfEqualOpcodeF0();
            _instructions[0xF1] = new SubtractInIndirectYModeOpcodeF1();
            //_instructions[0xF2]
            //_instructions[0xF4]
            _instructions[0xF5] = new SubtractInZeroPageXModeOpcodeF5();
            _instructions[0xF8] = new SetDecimalFlagOpcodeF8();
            _instructions[0xF9] = new SubtractInAbsoluteYModeOpcodeF9();
            //_instructions[0xFA]
            //_instructions[0xFC]
            _instructions[0xFD] = new SubtractInAbsoluteXModeOpcodeFD();
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