using NesCs.Logic.Ppu;

namespace NesCs.UnitTests.Ppu;

public class Ppu2C02Must
{
    [Fact]
    public void BeInitializedCorrectly()
    {
        var ramController = new RamControllerSpy();
        var sut = new Ppu2C02.Builder().WithRamController(ramController).Build();
        Assert.NotNull(sut.PpuCtrl);
        Assert.NotNull(sut.PpuMask);
        Assert.NotNull(sut.PpuStatus);
        Assert.NotNull(sut.OamAddr);
        Assert.NotNull(sut.OamData);
        Assert.NotNull(sut.PpuScroll);
        Assert.NotNull(sut.PpuAddr);
        Assert.NotNull(sut.PpuData);
        Assert.NotNull(sut.OamDma);
    }
}