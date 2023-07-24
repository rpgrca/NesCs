namespace NesCs.Logic.File;

public readonly struct NesFileOptions
{
    public bool LoadHeader { get; init; }
    public bool LoadTrainer { get; init; }
    public bool LoadCharacterRom { get; init; }
    public bool LoadProgramRom { get; init; }
}