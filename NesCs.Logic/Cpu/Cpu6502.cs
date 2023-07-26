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

        public Builder()
        {
            _p = _a = _x = _y = _s = 0;
            _ramSize =_start = _end = _pc = 0;
            _program = _ram = Array.Empty<byte>();
            _patch = Array.Empty<int[]>();
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

            return new Cpu6502(_program, _start, _end, _pc, _a, _x, _y, _s, _p, _ram);
        }
    }

    private byte _p;
    private byte _a;
    private int _pc;
    private byte _x;
    private byte _y;
    private byte _s;
    private byte[] _program;
    private int _start;
    private int _end;
    private byte[] _ram;
    private int _ip;

    private Cpu6502(byte[] program, int start, int end, int pc, byte a, byte x, byte y, byte s, byte p, byte[] ram)
    {
        _program = program;
        _start = start;
        _end = end;
        _pc = pc;
        _a = a;
        _x = x;
        _y = y;
        _s = s;
        _p = p;
        _ram = ram;
        _ip = 0;
    }

    [Obsolete("Replaced by builder")]
    public Cpu6502(byte[] program, int start, int end)
    {
        _program = program;
        _start = start;
        _end = end;
        _pc = 0;
        _a = 0;
        _ram = new byte[0xFFFF];

        PowerOn();
    }

    private void PowerOn()
    {
        _a = _x = _y = 0;
        _p = 0x34;
        _s = 0xFD;
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
        _ip = _start;
        while (_ip < _end)
        {
            var opcode = _program[_ip];
            _pc += 1;

            switch (opcode)
            {
                // LDA Load Accumulator with Memory
                // (indirect),Y   LDA (oper),Y   B1   2   5* 
                // OPC ($LL),Y	operand is zeropage address; effective address is word in (LL, LL + 1) incremented by Y with carry: C.w($00LL) + Y

                case 0xB1:
                    _ip += 1;
                    var address = _program[_ip];
                    _pc += 1;

                    var value = _ram[address] + _y;
                    _pc += 1;

                    value = _ram[value];
                    _pc += 1;
                    break;
            }
        }
    }

    private int GetCurrentOpcode() => _program[_p++ - _start];

    private byte GetByte() => _program[_p++ - _start];
}