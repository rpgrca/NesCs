namespace NesCs.Logic.Ppu;

public class AddressRegister
{
    private const int AddressIndex = 0x2006;
    private byte _index = 0;
    private byte[] _address = { 0, 0 };

    public byte UpperByte => (byte)(_address[0] % 0x40);
    public byte LowerByte => _address[1];

    public byte Address
    {
        private get => 0;
        set
        {
            _address[_index] = value;
            _index = (byte)((_index + 1) & 1);
        }
    }

    public void IncrementBy(byte value)
    {
        if (LowerByte + value > 0xff)
        {
            _address[0] = (byte)(_address[0] + 1);
        }

        _address[1] = (byte)(_address[1] + value);
    }

    public void Write(byte value, byte[] ram, IPpu ppu)
    {
        Address = value;
        ram[AddressIndex] = value;
    }

    public int CurrentAddress => UpperByte << 8 | LowerByte;

}