namespace NesCs.Logic.Cpu;

public partial class Cpu6502
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
            if (_ramSize < 1) _ramSize = 0x10000;
            if (_ram.Length < 1) _ram = new byte[_ramSize];
            if (_end < 1) _end = _program.Length;

            foreach ((int address, byte value) in _patch.Select(v => (v[0], (byte)v[1])))
            {
                _ram[address] = value;
            }

            return new Cpu6502(_program, _start, _end, _pc, _a, _x, _y, _s, _p, _ram, _trace);
        }
    }
}