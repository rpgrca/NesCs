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

    public void Call(int index, byte value, byte[] ram) => throw new NotImplementedException();

    public bool CanHandle(int index) => false;

    public void IncrementAddress() => throw new NotImplementedException();

    public byte Read() => throw new NotImplementedException();

    public byte Read(int index) => throw new NotImplementedException();

    public void Write(byte value) => throw new NotImplementedException();
}
