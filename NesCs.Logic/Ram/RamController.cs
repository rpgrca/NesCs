namespace NesCs.Logic.Ram;

public class RamController : IRamController
{
    public class Builder
    {
        private int _ramSize;
        private byte[] _ram;
        private Dictionary<int, Action<int, byte>> _callbacks;

        public Builder()
        {
            _callbacks = new();
            _ram = Array.Empty<byte>();
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

        public RamController Build()
        {
            _ramSize = _ramSize > 0? _ramSize : 0x10000;
            _ram = _ram.Length > 0? _ram : new byte[_ramSize];
            return new RamController(_ram, _callbacks);
        }
    }

    private readonly byte[] _ram;
    private readonly Dictionary<int, Action<int, byte>> _callbacks;
    private IRamHook? _ppuHook;

    public RamController(byte[] ram, Dictionary<int, Action<int, byte>> callbacks)
    {
        _ram = ram;
        _callbacks = callbacks;
    }

    public void RegisterHook(IRamHook hook) => _ppuHook = hook;

    public byte this[int index]
    {
        get => _ppuHook?.CanHandle(index) ?? false
                ? _ppuHook.Read(index)
                : _ram[index];
        set
        {
            _ram[index] = value;
            if (_ppuHook?.CanHandle(index) ?? false)
            {
                _ppuHook.Write(value);
            }

            if (_callbacks.ContainsKey(index))
            {
                _callbacks[index](index, value);
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