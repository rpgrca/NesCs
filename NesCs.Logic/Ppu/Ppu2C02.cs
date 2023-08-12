namespace NesCs.Logic.Ppu;

public class Ppu2C02
{
    public ControlRegister PpuCtrl { get; }
    public Mask PpuMask { get; }
    public Status PpuStatus { get; }
    public ObjectAttributeMemory OamData { get; }
    public Scroll PpuScroll { get; }
    public Address PpuAddr { get; }
    public Data PpuData { get; }
    public ObjectAttributeMemory OamDma { get; }

    public Ppu2C02()
    {
        PpuCtrl = new ControlRegister();
        PpuMask = new Mask();
        PpuStatus = new Status();
        OamData = new ObjectAttributeMemory();
        PpuScroll = new Scroll();
        PpuAddr = new Address();
        PpuData = new Data();
        OamDma = new ObjectAttributeMemory();
    }
}