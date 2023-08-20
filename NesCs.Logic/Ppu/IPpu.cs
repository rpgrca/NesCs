using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public interface IPpu : IRamHook, IPpuVram
{
    ControlRegister PpuCtrl { get; }
    Mask PpuMask { get; }
    Status PpuStatus { get; }
    OamAddressPort OamAddr { get; }
    OamDataPort OamData { get; }
    ScrollingPositionRegister PpuScroll { get; }
    AddressRegister PpuAddr { get; }
    DataPort PpuData { get; }
    OamDmaRegister OamDma { get; }

}

public interface IPpuVram
{
    int CurrentAddress { get; }
    void Write(byte value);
    byte Read();
    void IncrementAddress();
}