using NesCs.Logic.File;

namespace NesCs.Logic;

internal class ArchaicINesFile : INesFile
{
    private const int HeaderSignatureIndex = 0;
    private const int HeaderProgramSizeIndex = 4;
    private const int HeaderCharacterSizeIndex = 5;
    private const int HeaderFlags6Index = 6;

    protected readonly byte[] _contents;

    public string Filename { get; }
    public int ProgramRomSize { get; private set; }
    public int CharacterRomSize { get; private set; }
    public int MapperNumber { get; protected set; }
    public Flags6 Flags6 { get; private set; }
    public Flags7 Flags7 { get; protected set; }
    public Flags8 Flags8 { get; protected set; }
    public Flags9 Flags9 { get; protected set; }
    public Flags10 Flags10 { get; protected set; }
    public Byte13 Byte13 { get; protected set; }
    public Byte15 Byte15 { get; protected set; }

    internal ArchaicINesFile(string filename, byte[] contents)
    {
        Filename = filename;
        _contents = contents.ToArray();

        ParseHeader();
    }

    private void ParseHeader()
    {
        if (_contents.Length < 16) throw new ArgumentException("Could not find header", nameof(_contents));

        LoadSignature();
        LoadProgramSize();
        LoadCharacterSize();
        LoadFlags6();
        LoadFlags7();
        LoadMapperNumber();
        LoadFlags8();
        LoadFlags9();
        LoadFlags10();
        LoadByte11();
        LoadByte12();
        LoadByte13();
        LoadByte14();
        LoadByte15();
    }

    private void LoadSignature()
    {
        byte[] headerSignature = { 0x4e, 0x45, 0x53, 0x1A };
        if (! _contents[HeaderSignatureIndex..HeaderProgramSizeIndex].SequenceEqual(headerSignature))
        {
            throw new ArgumentException("Signature not found", nameof(_contents));
        }
    }

    private void LoadProgramSize() => ProgramRomSize = _contents[HeaderProgramSizeIndex];

    private void LoadCharacterSize() => CharacterRomSize = _contents[HeaderCharacterSizeIndex];

    private void LoadFlags6() => Flags6 = new Flags6(_contents[HeaderFlags6Index]);

    protected virtual void LoadFlags7() => Flags7 = new Flags7(0);

    protected virtual void LoadMapperNumber()
    {
    }

    protected virtual void LoadFlags8() => Flags8 = new Flags8();

    protected virtual void LoadFlags9() => Flags9 = new Flags9();

    protected virtual void LoadFlags10() => Flags10 = new Flags10();

    protected virtual void LoadByte11()
    {
    }

    protected virtual void LoadByte12()
    {
    }

    protected virtual void LoadByte13() => Byte13 = new Byte13();

    protected virtual void LoadByte14()
    {
    }

    protected virtual void LoadByte15()
    {
    }
}