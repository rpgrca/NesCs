using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ppu;

public class AddressRegisterMust
{
    [Fact]
    public void InitializeAddressCorrectly()
    {
        var ramController = new RamControllerSpy();
        var sut = new AddressRegister(ramController);
        Assert.Equal(0, sut.CurrentAddress);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1 << 8)]
    [InlineData(0x30, 0x30 << 8)]
    public void SetAddressUpperByteCorrectly(byte value, int expectedValue)
    {
        var ramController = new RamControllerSpy();
        var sut = new AddressRegister(ramController);
        sut.Write(value);
        Assert.Equal(expectedValue, sut.CurrentAddress);
    }

    [Theory]
    [InlineData(0, 4 << 8 | 0)]
    [InlineData(1, 4 << 8 | 1)]
    [InlineData(0xff, 4 << 8 | 0xff)]
    public void SetAddressLowerByteCorrectly(byte value, int expectedValue)
    {
        var ramController = new RamControllerSpy();
        var sut = new AddressRegister(ramController);
        sut.Write(0x4);
        sut.Write(value);
        Assert.Equal(expectedValue, sut.CurrentAddress);
    }

    [Theory]
    [InlineData(0x40, 0)]
    [InlineData(0x50, 0x10 << 8)]
    public void MirrorDownValue_WhenAddressGoesAbove3fff(byte value, int expectedValue)
    {
        var ramController = new RamController.Builder().Build();
        var sut = new AddressRegister(ramController);
        sut.Write(value);
        sut.Write(0x00);
        Assert.Equal(expectedValue, sut.CurrentAddress);
    }

    [Theory]
    [InlineData(0x01, 0x20, 0, 0x01 << 8 | 0x21)]
    [InlineData(0x01, 0x20, 1, 0x01 << 8 | 0x40)]
    [InlineData(0x01, 0xff, 0, 0x02 << 8 | 0x00)]
    [InlineData(0x01, 0xff, 1, 0x02 << 8 | 0x1f)]
    public void IncrementAddressCorrectly(byte high, byte low, byte flag, int expectedAddress)
    {
        var ramController = new RamController.Builder().Build();
        var sut = new Ppu2C02.Builder().WithRamController(ramController).Build();
        sut.PpuCtrl.I = flag;
        sut.PpuAddr.Write(high);
        sut.PpuAddr.Write(low);
        sut.PpuData.Write(0xff);

        Assert.Equal(expectedAddress, sut.PpuAddr.CurrentAddress);
    }
}