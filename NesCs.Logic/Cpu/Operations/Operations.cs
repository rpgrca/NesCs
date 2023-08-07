namespace NesCs.Logic.Cpu.Operations;

public class Operations
{
    public IOperation AddWithCarry { get; }

    public Operations()
    {
        AddWithCarry = new AddWithCarry();
    }
}
