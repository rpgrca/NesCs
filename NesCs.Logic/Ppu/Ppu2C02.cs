namespace NesCs.Logic.Ppu;

public class Ppu2C02
{
    public ControlRegister PpuCtrl { get; } /* 0x2000 */
    public Mask PpuMask { get; }            /* 0x2001 */
    public Status PpuStatus { get; }        /* 0x2002 */
    public OamAddressPort OamAddr { get; }  /* 0x2003 */
    public OamDataPort OamData { get; }     /* 0x2004 */
    public ScrollingPositionRegister PpuScroll { get; }        /* 0x2005 */
    public AddressRegister PpuAddr { get; } /* 0x2006 */
    public DataPort PpuData { get; }        /* 0x2007 */
    public OamDmaRegister OamDma { get; }   /* 0x4014 */

    public Ppu2C02()
    {
        PpuCtrl = new ControlRegister();
        PpuMask = new Mask();
        PpuStatus = new Status();
        OamAddr = new OamAddressPort();
        OamData = new OamDataPort(OamAddr);
        PpuScroll = new ScrollingPositionRegister();
        PpuAddr = new AddressRegister();
        PpuData = new DataPort();
        OamDma = new OamDmaRegister();
    }
}