using NesCs.Logic.Ppu;

namespace NesCs.UnitTests.Ppu;

public class ByteToggleSpy : IByteToggle
{
    public bool Toggled { get; private set; }

    public int GetIndex() => 0;

    public void Reset() => Toggled = true;
}
