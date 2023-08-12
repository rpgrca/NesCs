using NesCs.Logic.Ppu;

namespace NesCs.UnitTests.Ppu;

public class Ppu2C02Must
{
    [Fact]
    public void BeInitializedCorrectly()
    {
        var sut = new Ppu2C02();
        Assert.NotNull(sut.PpuCtrl);
        Assert.NotNull(sut.PpuMask);
        Assert.NotNull(sut.PpuStatus);
        Assert.NotNull(sut.OamData);
        Assert.NotNull(sut.PpuScroll);
        Assert.NotNull(sut.PpuAddr);
        Assert.NotNull(sut.PpuData);
        Assert.NotNull(sut.OamDma);
    }
}