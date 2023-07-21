namespace NesCs.Logic;

public interface INesFile
{
    string Filename { get; }
    int ProgramRomSize { get; }
    int CharacterRomSize { get; }
}