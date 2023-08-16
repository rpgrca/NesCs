namespace NesCs.Logic.Ppu;

public class OamAddressPort
{
    public byte Address { get; set; }

    internal void IncrementAddress() => Address++;

    public void Write(byte value) => Address = value;
}