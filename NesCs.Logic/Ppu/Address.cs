namespace NesCs.Logic.Ppu;

public class AddressRegister
{
    private byte _index = 0;
    private byte[] _address = { 0, 0 };

    public byte UpperByte => _address[0];
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
}