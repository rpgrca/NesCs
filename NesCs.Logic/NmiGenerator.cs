namespace NesCs.Logic.Cpu;

public class NmiGenerator : INmiGenerator
{
    private byte _controlSet;
    private byte _statusSet;
    private Cpu6502? _cpu;

    public void SetControl(byte value)
    {
        _controlSet = value;
        CheckForNmi();
    }

    public void SetStatus(byte value)
    {
        _statusSet = value;
        CheckForNmi();
    }

    private void CheckForNmi()
    {
        if (_controlSet + _statusSet == 2)
        {
            _cpu?.SetNmiFlipFlop();
        }
    }

    public void AttachTo(Cpu6502 cpu) => _cpu = cpu;
}
