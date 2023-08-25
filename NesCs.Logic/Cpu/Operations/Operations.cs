using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu.Operations;

public class Operations
{
    public IOperation AddWithCarry { get; }
    public IOperation BitTest { get; }
    public IAndFactory And { get; }
    public IOperation Or { get; }
    public IOperation Xor { get; }
    public IShiftLeftFactory ShiftLeft { get; }
    public IShiftRightFactory ShiftRight { get; }
    public IOperation Nop { get; }
    public ICompareFactory Compare { get; }
    public IOperation SubtractWithCarry { get; }
    public IFlagOperation Flag { get; }
    public IRotateFactory RotateLeft { get; }
    public IRotateFactory RotateRight { get; }
    public IAutocrementFactory Decrement { get; internal set; }
    public IAutocrementFactory Increment { get; internal set; }
    public ILoadFactory Load { get; internal set; }
    public IStoreFactory Store { get; }
    public ITransferFactory Transfer { get; }
    public IOperation Jump { get; }
    public IBranchFactory Branch { get; }

    public Operations()
    {
        AddWithCarry = new AddWithCarry();
        BitTest = new BitTest();
        And = new AndFactory();
        Or = new Or();
        Xor = new Xor();
        ShiftLeft = new ShiftLeftFactory();
        ShiftRight = new ShiftRightFactory();
        Nop = new Nop();
        Compare = new CompareFactory();
        SubtractWithCarry = new SubtractWithCarry();
        Flag = new FlagOperation();
        RotateLeft = new RotateFactory((b, a) => new RotateLeft(b, a));
        RotateRight = new RotateFactory((b, a) => new RotateRight(b, a));
        Decrement = new AutocrementFactory(v => (byte)(v - 1));
        Increment = new AutocrementFactory(v => (byte)(v + 1));
        Load = new LoadFactory();
        Store = new StoreFactory();
        Transfer = new TransferFactory();
        Jump = new Jump();
        Branch = new BranchFactory();
    }
}