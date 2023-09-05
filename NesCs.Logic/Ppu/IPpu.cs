using NesCs.Logic.Cpu.Clocking;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public interface IPpu : IRamHook, IPpuVram, IPpuTiming, IClockHook
{
    ControlRegister PpuCtrl { get; }                /* 0x2000 W  */
    Mask PpuMask { get; }                           /* 0x2001 W  */
    Status PpuStatus { get; }                       /* 0x2002  R */
    OamAddressPort OamAddr { get; }                 /* 0x2003 W  */
    OamDataPort OamData { get; }                    /* 0x2004 WR */
    ScrollingPositionRegister PpuScroll { get; }    /* 0x2005 W  */
    AddressRegister PpuAddr { get; }                /* 0x2006 W  */
    DataPort PpuData { get; }                       /* 0x2007 WR */
    OamDmaRegister OamDma { get; }                  /* 0x4014 W  */

}