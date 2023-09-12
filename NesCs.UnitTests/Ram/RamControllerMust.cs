using NesCs.Logic.Ram;

namespace NesCs.UnitTests.Ram;

public class RamControllerMust
{
    [Theory]
    [InlineData(0x0173)]
    [InlineData(0x0973)]
    [InlineData(0x1173)]
    [InlineData(0x1973)]
    public void MirrorSystemMemory0x0000to0x1FFFCorrectly(int address)
    {
        const byte Value = 0xBE;
        var sut = new RamController.Builder().Build();
        sut[address] = 0xBE;
        Assert.Equal(Value, sut[0x0173]);
        Assert.Equal(Value, sut[0x0973]);
        Assert.Equal(Value, sut[0x1173]);
        Assert.Equal(Value, sut[0x1973]);
    }
}