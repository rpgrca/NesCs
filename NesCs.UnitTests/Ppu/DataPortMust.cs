namespace NesCs.UnitTests.Ppu;

public class DataPortMust
{
    [Fact]
    public void SetDataCorrectly()
    {
        var sut = new Logic.Ppu.DataPort
        {
            Data = 0x13
        };

        Assert.Equal(0x13, sut.Data);
    }
}