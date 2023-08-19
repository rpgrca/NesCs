using NesCs.Logic.Ppu;

namespace NesCs.UnitTests.Ppu;

public class DataPortMust
{
    [Fact]
    public void SetDataCorrectly()
    {
        var vram = new byte[100];
        var ppu = new Ppu2C02.Builder().WithVram(vram).Build();
        var sut = new DataPort(ppu);
        sut.Write(0x13);
    }
}