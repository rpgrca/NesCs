namespace NesCs.Logic.Cpu.Operations;

public class Operations
{
    public IOperation AddWithCarry { get; }
    public IOperation BitTest { get; }
    public IOperation And { get; }
    public IOperation Or { get; }
    public IOperation ShiftLeft { get; }
    public IOperation Nop { get; }
    public ICompareFactory Compare { get; }
    public IOperation SubtractWithCarry { get; }
    public IFlagOperation Flag { get; }

    public Operations()
    {
        AddWithCarry = new AddWithCarry();
        BitTest = new BitTest();
        And = new And();
        Or = new Or();
        ShiftLeft = new ShiftLeft();
        Nop = new Nop();
        Compare = new CompareFactory();
        SubtractWithCarry = new SubtractWithCarry();
        Flag = new FlagOperation();
    }

}