namespace NesCs.Logic.Ppu;

public interface IPpuTiming
{
    void LoadHighBackgroundTileByte();
    void LoadLowBackgroundTileByte();
    void LoadAttributeByte();
    void LoadNametableByte();
}