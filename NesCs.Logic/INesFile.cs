namespace NesCs.Logic;

public interface INesFile
{
    string Filename { get; }
    int ProgramRomSize { get; }
    int CharacterRomSize { get; }
    Flags6 Flags6 { get; }
}