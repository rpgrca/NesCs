namespace NesCs.Logic;

public class NesFile : INesFile
{
    private static readonly byte[] HeaderSignature = new byte[] { 0x4e, 0x45, 0x53, 0x1A };
    private const int HeaderSignatureIndex = 0;
    private const int HeaderProgramSizeIndex = 4;
    private const int HeaderCharacterSizeIndex = 5;
    private const int HeaderFlags6Index = 6;
    private const int HeaderFlags7Index = 7;
    private const int HeaderFlags8Index = 8;

    private readonly byte[] _contents;

    public string Filename { get; }
    public int ProgramRomSize { get; private set; }
    public int CharacterRomSize { get; private set; }
    public int MapperNumber { get; private set; }
    public Flags6 Flags6 { get; private set; }
    public Flags7 Flags7 { get; private set; }
    public Flags8 Flags8 { get; private set; }

    internal NesFile(string filename, byte[] contents)
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
    }

    private void LoadSignature()
    {
        if (! _contents[HeaderSignatureIndex..HeaderProgramSizeIndex].SequenceEqual(HeaderSignature))
        {
            throw new ArgumentException("Signature not found", nameof(_contents));
        }
    }

    private void LoadProgramSize() =>
        ProgramRomSize = _contents[HeaderProgramSizeIndex];

    private void LoadCharacterSize() =>
        CharacterRomSize = _contents[HeaderCharacterSizeIndex];

    private void LoadFlags6() =>
        Flags6 = new Flags6(_contents[HeaderFlags6Index]);

    private void LoadFlags7() =>
        Flags7 = new Flags7(_contents[HeaderFlags7Index]);

    private void LoadMapperNumber() =>
        MapperNumber = (Flags7.UpperMapperNybble << 4) | Flags6.LowerMapperNybble;

    private void LoadFlags8() =>
        Flags8 = new Flags8(_contents[HeaderFlags8Index]);
}