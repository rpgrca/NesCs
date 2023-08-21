using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public interface IPpu : IRamHook, IPpuVram
{
    ControlRegister PpuCtrl { get; }                /* 0x2000 */
    Mask PpuMask { get; }                           /* 0x2001 */
    Status PpuStatus { get; }                       /* 0x2002 */
    OamAddressPort OamAddr { get; }                 /* 0x2003 */
    OamDataPort OamData { get; }                    /* 0x2004 */
    ScrollingPositionRegister PpuScroll { get; }    /* 0x2005 */
    AddressRegister PpuAddr { get; }                /* 0x2006 */
    DataPort PpuData { get; }                       /* 0x2007 */
    OamDmaRegister OamDma { get; }                  /* 0x4014 */

}