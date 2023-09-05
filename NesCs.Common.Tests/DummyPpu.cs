using NesCs.Logic.Cpu.Clocking;
using NesCs.Logic.Ppu;

namespace NesCs.Common.Tests;

public class DummyPpu : IPpu
{
    public ControlRegister PpuCtrl => throw new NotImplementedException();

    public Mask PpuMask => throw new NotImplementedException();

    public Status PpuStatus => throw new NotImplementedException();

    public OamAddressPort OamAddr => throw new NotImplementedException();

    public OamDataPort OamData => throw new NotImplementedException();

    public ScrollingPositionRegister PpuScroll => throw new NotImplementedException();

    public AddressRegister PpuAddr => throw new NotImplementedException();

    public DataPort PpuData => throw new NotImplementedException();

    public OamDmaRegister OamDma => throw new NotImplementedException();

    public int CurrentAddress => throw new NotImplementedException();

    public int MasterClockDivisor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Call(int index, byte value, byte[] ram) => throw new NotImplementedException();

    public bool CanHandle(int index) => false;

    public void IncrementAddress() => throw new NotImplementedException();

    void IPpuVram.Write(byte value) => throw new NotImplementedException();

    byte IPpuVram.Read() => throw new NotImplementedException();

    byte Logic.Ram.IRamHook.Read(int index) => throw new NotImplementedException();

    void Logic.Ram.IRamHook.Write(int index, byte value) => throw new NotImplementedException();

    string IClockHook.GetStatus() => throw new NotImplementedException();

    bool IClockHook.Trigger(IClock clock) => throw new NotImplementedException();

    void IPpuTiming.LoadNametableByte() => throw new NotImplementedException();

    void IPpuTiming.LoadHighBackgroundTileByte() => throw new NotImplementedException();

    void IPpuTiming.LoadLowBackgroundTileByte() => throw new NotImplementedException();

    void IPpuTiming.LoadAttributeByte() => throw new NotImplementedException();
}
