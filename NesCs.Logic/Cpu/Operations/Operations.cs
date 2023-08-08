namespace NesCs.Logic.Cpu.Operations;

public class Operations
{
    public IOperation AddWithCarry { get; }
    public IOperation BitTest { get; }
    public IOperation And { get; }
    public IOperation Or { get; }
    public IOperation ShiftLeft { get; }
    public IOperation ClearFlag { get; }
    public IOperation Nop { get; }
    public ICompareFactory Compare { get; }

    public Operations()
    {
        AddWithCarry = new AddWithCarry();
        BitTest = new BitTest();
        And = new And();
        Or = new Or();
        ShiftLeft = new ShiftLeft();
        ClearFlag = new ClearFlag();
        Nop = new Nop();
        Compare = new CompareFactory();
    }

}