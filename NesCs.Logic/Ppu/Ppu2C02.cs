using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Ppu2C02 : IPpu
{
    private readonly byte[] _map;
    private readonly OamSprite[] _oam;
    private readonly OamSprite[] _secondaryOam;
    
    public ControlRegister PpuCtrl { get; } /* 0x2000 */
    public Mask PpuMask { get; }            /* 0x2001 */
    public Status PpuStatus { get; }        /* 0x2002 */
    public OamAddressPort OamAddr { get; }  /* 0x2003 */
    public OamDataPort OamData { get; }     /* 0x2004 */
    public ScrollingPositionRegister PpuScroll { get; }        /* 0x2005 */
    public AddressRegister PpuAddr { get; } /* 0x2006 */
    public DataPort PpuData { get; }        /* 0x2007 */
    public OamDmaRegister OamDma { get; }   /* 0x4014 */

    public Ppu2C02(IRamController ram)
    {
        _map = new byte[0x4000];
        _oam = new OamSprite[64];
        _secondaryOam = new OamSprite[8];

        PpuCtrl = new ControlRegister(ram);
        PpuMask = new Mask(ram);
        PpuStatus = new Status(ram);
        OamAddr = new OamAddressPort();
        OamData = new OamDataPort(OamAddr );
        PpuScroll = new ScrollingPositionRegister();
        PpuAddr = new AddressRegister();
        PpuData = new DataPort();
        OamDma = new OamDmaRegister();
    }

    public void Call(int index, byte value, byte[] ram)
    {
        switch (index)
        {
            case 0x2000: PpuCtrl.Write(value, ram); break;
            case 0x2001: PpuMask.Write(value, ram); break;
            case 0x2002: PpuStatus.Write(value, ram); break;
            case 0x2003: OamAddr.Write(value, ram); break;
            case 0x2004: OamData.Write(value, ram); break;
            case 0x2005: PpuScroll.Write(value, ram); break;
            case 0x2006: PpuAddr.Write(value, ram); break;
            case 0x2007: PpuData.Write(value, ram); break;
        }
    }

    public bool CanHandle(int index) => index >= 0x2000 && index <= 0x2007;
}