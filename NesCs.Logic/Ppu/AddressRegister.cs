using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class AddressRegister
{
    private const int AddressIndex = 0x2006;
    private readonly byte[] _address = { 0, 0 };
    private readonly IRamController _ram;
    private byte _index;

    public int CurrentAddress { get; private set; }

    public AddressRegister(IRamController ram)
    {
        _ram = ram;
        _index = 0;
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
        if (_index == 0)
        {
            _address[0] = (byte)(value & 0b00111111);
            _index = 1;
        }
        else
        {
            _address[1] = value;
            _index = 0;
            CalculateCurrentAddress();
        }

        Address = value;
    }

    public byte Read() => Address;
}