namespace NesCs.Logic.Ram;

public class RamController : IRamController
{
    public class Builder
    {
        private int _ramSize;
        private Dictionary<int, Action<int, byte>> _callbacks;

        public Builder() => _callbacks = new();

        public Builder WithRamSizeOf(int size)
        {
            _ramSize = size;
            return this;
        }

        public RamController Build()
        {
            _ramSize = _ramSize > 0? _ramSize : 0x10000;
            return new RamController(_ramSize, _callbacks);
        }
    }

    private readonly byte[] _ram;
    private readonly Dictionary<int, Action<int, byte>> _callbacks;
    private IRamHook? _ppuHook;

    public RamController(int ramSize, Dictionary<int, Action<int, byte>> callbacks)
    {
        _ram = new byte[ramSize];
        _callbacks = callbacks;
    }

    public void RegisterHook(IRamHook hook) => _ppuHook = hook;

    public byte this[int index]
    {
        get => _ram[index];
        set
        {
            _ram[index] = value;
            if (_ppuHook?.CanHandle(index) ?? false)
            {
                _ppuHook.Call(index, value, _ram);
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
}