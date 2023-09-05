using System.Diagnostics;

namespace NesCs.Logic.Ppu;

[DebuggerDisplay("({Y}, {X})")]
public class RasterAddress
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public RasterAddress() => X = Y = 0;
/*
    public void IncrementX()
    {
        X += 1;
        if (X > 340)
        {
            X = 0;
            Y += 1;

            if (Y > 261)
            {
                Y = 0;
            }
        }
    }*/

    internal void BackToOrigin()
    {
        Y = 0;
        X = 0;
    }

    internal void ResetX() => X = 0;

    internal void ResetY() => Y = 0;

    internal void IncrementX() => X++;

    internal void IncrementY() => Y++;
}