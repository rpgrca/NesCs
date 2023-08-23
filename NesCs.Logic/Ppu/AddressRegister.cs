using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class AddressRegister
{
    public const int AddressIndex = 0x2006;
    private readonly byte[] _address = { 0, 0 };
    private readonly IRamController _ram;
    private readonly IByteToggle _toggle;
    private readonly IPpuIOBus _ioBus;

    public int CurrentAddress { get; private set; }

    public AddressRegister(IRamController ram, IByteToggle toggle, IPpuIOBus ioBus)
    {
        _ram = ram;
        _toggle = toggle;
        _ioBus = ioBus;
    }

    private byte Address
    {
        get => _ram.DirectReadFrom(AddressIndex);
        set => _ram.DirectWriteTo(AddressIndex, value);
    }

    public void IncrementBy(byte value)
    {
        if (_address[1] + value > 0xff)
        {
            _address[0] = (byte)(_address[0] + 1);
        }

        _address[1] = (byte)(_address[1] + value);
        CalculateCurrentAddress();
    }

    private void CalculateCurrentAddress() =>
        CurrentAddress = _address[0] << 8 | _address[1];

    public void Write(byte value)
    {
        _ioBus.Write(value);
        var index = _toggle.GetIndex();
        if (index == 0)
        {
            _address[0] = (byte)(value & 0b00111111);
        }
        else
        {
            _address[1] = value;
            CalculateCurrentAddress();
        }

        Address = value;
    }

    public byte Read() => _ioBus.Read();
}