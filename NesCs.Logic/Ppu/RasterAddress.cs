using System.Diagnostics;

namespace NesCs.Logic.Ppu;

[DebuggerDisplay("({Y}, {X})")]
public class RasterAddress
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool IgnoringVblank { get; private set; }

    public RasterAddress() => X = Y = 0;

    internal void BackToOrigin()
    {
        Y = 0;
        X = 0;
        IgnoringVblank = false;
    }

    internal void ResetX() => X = 0;

    internal void IncrementX() => X++;

    internal void IncrementY() => Y++;

    internal bool IsVblSetNextCycle() => Y == 241 && X == 0;

    internal bool IsVblJustSet() => Y == 241 && (X == 1 || X == 2);

    internal void IgnoreVblankThisFrame() => IgnoringVblank = true;
}