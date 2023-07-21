namespace NesCs.Logic;

public interface INesFile
{
    string Filename { get; }
    int ProgramRomSize { get; }
    int CharacterRomSize { get; }
    int MapperNumber { get; }
    Flags6 Flags6 { get; }
    Flags7 Flags7 { get; }
    Flags8 Flags8 { get; }
    Flags9 Flags9 { get; }
    Flags10 Flags10 { get; }
}