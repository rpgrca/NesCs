using NesCs.Logic.Ppu;

namespace NesCs.Logic.Ram;

public class RamController : IRamController
{
    public class Builder
    {
        private int _ramSize;

        public Builder WithRamSizeOf(int size)
        {
            _ramSize = size;
            return this;
        }

        public RamController Build()
        {
            _ramSize = _ramSize > 0? _ramSize : 0x10000;
            return new RamController(_ramSize);
        }
    }

    private readonly byte[] _ram;
    private IRamHook? _ppuHook;

    public RamController(int ramSize)
    {
        _ram = new byte[ramSize];
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
        }
    }

    public void Copy(byte[] program, int startIndex, int memoryOffset, int programSize) =>
        Array.Copy(program, 0, _ram, memoryOffset, programSize);
}