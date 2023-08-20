using NesCs.Logic.Ppu;

namespace NesCs.UnitTests.Ppu;

public class DataPortMust
{
    [Fact]
    public void SetDataCorrectly_WhenAddressIsZero()
    {
        var vram = new byte[100];
        var ppu = new Ppu2C02.Builder().WithVram(vram).Build();
        ppu.PpuData.Write(0x13);
        Assert.Equal(0x13, vram[0]);
    }

    [Fact]
    public void SetDataCorrectly_WhenAddressIsNotZero()
    {
        var vram = new byte[100];
        var ppu = new Ppu2C02.Builder().WithVram(vram).Build();
        ppu.PpuAddr.Write(0x00);
        ppu.PpuAddr.Write(0x10);

        ppu.PpuData.Write(0x13);
        Assert.Equal(0x13, vram[0x10]);
    }

    [Fact]
    public void ReturnCachedValue_WhenReadingDataBeforePalettes()
    {
        var vram = Enumerable.Range(1, 100).Select(p => (byte)p).ToArray();
        var ppu = new Ppu2C02.Builder().WithVram(vram).Build();
        ppu.PpuAddr.Write(0x00);
        ppu.PpuAddr.Write(0x00);
        Assert.Equal(0, ppu.PpuData.Read());
        Assert.Equal(1, ppu.PpuData.Read());
    }

    [Fact]
    public void RefreshCachedValue_WhenReadingDataBeforePalettes()
    {
        var vram = Enumerable.Range(1, 100).Select(p => (byte)p).ToArray();
        var ppu = new Ppu2C02.Builder().WithVram(vram).Build();
        ppu.PpuAddr.Write(0x00);
        ppu.PpuAddr.Write(0x00);
        Assert.Equal(0, ppu.PpuData.Read());
        Assert.Equal(1, ppu.PpuData.Read());
    }

    [Theory]
    [InlineData(0x3F, 0x00, 0x0)]
    [InlineData(0x3F, 0x01, 0x1)]
    public void Test1(byte high, byte low, byte expectedValue)
    {
        var vram = Enumerable.Range(0, 0x3FFF).Select(p => (byte)p).ToArray();
        var ppu = new Ppu2C02.Builder().WithVram(vram).Build();
        ppu.PpuAddr.Write(high);
        ppu.PpuAddr.Write(low);
        Assert.Equal(expectedValue, ppu.PpuData.Read());
    }
}