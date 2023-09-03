namespace NesCs.Logic.Ppu;

public class RasterAddress
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public RasterAddress() => X = Y = 0;

    public void IncrementX() => X += 1;

    public void ResetX() => X = 0;

    internal void IncrementY() => Y += 1;

    internal void ResetY() => Y = 0;
}