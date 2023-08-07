namespace NesCs.Logic.Cpu.Operations;

public class Operations
{
    public IOperation AddWithCarry { get; }
    public IOperation BitTest { get; }
    public IOperation And { get; }
    public IOperation Or { get; }

    public Operations()
    {
        AddWithCarry = new AddWithCarry();
        BitTest = new BitTest();
        And = new And();
        Or = new Or();
    }
}