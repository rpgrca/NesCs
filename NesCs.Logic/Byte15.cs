namespace NesCs.Logic;

public readonly struct Byte15
{
    public ExpansionDevice ExpansionDevice { get; }

    public Byte15(int flags)
    {
        ExpansionDevice = (ExpansionDevice)(flags & 0b111111);
    }
}