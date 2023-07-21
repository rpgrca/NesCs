namespace NesCs.Logic;

public interface INesFile
{
    string Filename { get; }
    int ProgramRomSize { get; }
    int CharacterRomSize { get; }
    int MapperNumber { get; }
    Flags6 Flags6 { get; }
    Flags7 Flags7 { get; }
}