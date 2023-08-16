namespace NesCs.Logic.Ram;

public class RamController
{
    public class Builder
    {
        private int _ramSize;
        private IRamHook? _ppuHook;

        public Builder WithRamSizeOf(int size)
        {
            _ramSize = size;
            return this;
        }

        public Builder HookingPpuAt(IRamHook hook)
        {
            _ppuHook = hook;
            return this;
        }

        public RamController Build()
        {
            _ramSize = _ramSize > 0? _ramSize : 0x10000;
            _ppuHook ??= new Ppu.Ppu2C02();
            return new RamController(_ramSize, _ppuHook);
        }
    }

    private readonly byte[] _ram;
    private readonly IRamHook _ppuHook;

    public RamController(int ramSize, IRamHook ppuHook)
    {
        _ram = new byte[ramSize];
        _ppuHook = ppuHook;
    }

    public byte this[int index]
    {
        get => _ram[index];
        set
        {
            _ram[index] = value;
            if (_ppuHook.CanHandle(index))
            {
                _ppuHook.Call(index, value, _ram);
            }
        }
    }
}