namespace NesCs.Logic.Ram;

public class RamController : IRamController
{
    public class Builder
    {
        private int _ramSize;
        private byte[] _ram;
        private bool _makeRomReadWrite;
        private Dictionary<int, Action<int, byte>> _callbacks;
        private Action<byte[], int, byte> _mirroring;

        public Builder()
        {
            _callbacks = new();
            _ram = Array.Empty<byte>();
            _makeRomReadWrite = true;
            _mirroring = (r, i, v) =>
            {
                var offset = i & 0x07FF;
                r[0x000 + offset] = r[0x0800 + offset] = r[0x1000 + offset] = r[0x1800 + offset] = v;
            };
        }

        public Builder WithoutMirroring()
        {
            _mirroring = (r, i, v) => r[i] = v;
            return this;
        }

        public Builder WithRamSizeOf(int size)
        {
            _ramSize = size;
            return this;
        }

        public Builder WithRamOf(byte[] ram)
        {
            _ram = ram;
            return this;
        }

        public Builder PreventRomRewriting()
        {
            _makeRomReadWrite = false;
            return this;
        }

        public RamController Build()
        {
            _ramSize = _ramSize > 0? _ramSize : 0x10000;
            _ram = _ram.Length > 0? _ram : new byte[_ramSize];
            return new RamController(_ram, _makeRomReadWrite, _callbacks, _mirroring);
        }
    }

    private readonly byte[] _ram;
    private readonly bool _makeRomReadWrite;
    private readonly Dictionary<int, Action<int, byte>> _callbacks;
    private readonly Action<byte[], int, byte> _mirroring;
    private IRamHook? _ppuHook;

    public RamController(byte[] ram, bool makeRomReadWrite, Dictionary<int, Action<int, byte>> callbacks, Action<byte[], int, byte> mirroring)
    {
        _ram = ram;
        _makeRomReadWrite = makeRomReadWrite;
        _callbacks = callbacks;
        _mirroring = mirroring;
    }

    public void RegisterHook(IRamHook hook) => _ppuHook = hook;

    public byte this[int index]
    {
        get => _ppuHook?.CanHandle(index) ?? false
                ? _ppuHook.Read(index)
                : _ram[index];
        set
        {
            if (_callbacks.ContainsKey(index))
            {
                _callbacks[index](index, value);
            }

            if (_makeRomReadWrite || index < 0x8000)
            {
                if (index <= 0x1FFF)
                {
                    _mirroring(_ram, index, value);
                }
                else
                {
                    _ram[index] = value;
                }
            }

            if (_ppuHook?.CanHandle(index) ?? false)
            {
                _ppuHook.Write(index, value);
            }
        }
    }

    public void Copy(byte[] program, int startIndex, int memoryOffset, int programSize) =>
        Array.Copy(program, 0, _ram, memoryOffset, programSize);

    public void AddHook(int address, Action<int, byte> callback) =>
        _callbacks.Add(address, callback);

    public byte DirectReadFrom(int index) => _ram[index];

    public void DirectWriteTo(int index, byte value) => _ram[index] = value;
}