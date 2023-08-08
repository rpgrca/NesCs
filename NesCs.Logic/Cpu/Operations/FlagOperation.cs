namespace NesCs.Logic.Cpu.Operations;

public class FlagOperation : IFlagOperation
{
    public IFlagFactory Minus { get; }
    public IFlagFactory Plus { get; }

    public FlagOperation()
    {
        Minus = new FlagFactory(c => c.ClearDecimalMode(), c => c.ClearOverflowFlag(), c => c.ClearInterruptDisable(), c => c.ClearCarryFlag());
        Plus = new FlagFactory(c => c.SetDecimalFlag(), c => c.SetOverflowFlag(), c => c.SetInterruptDisable(), c => c.SetCarryFlag());
    }
}