namespace NesCs.Logic.File;

internal class ArchaicINesFile : INesFile
{
    private const int HeaderSignatureIndex = 0;
    private const int HeaderProgramSizeIndex = 4;
    private const int HeaderCharacterSizeIndex = 5;
    private const int HeaderFlags6Index = 6;
    private const int TrainerSize = 512;
    private const int ProgramRomBlockSize = 16384;
    private const int CharacterRomBlockSize = 8192;

    protected readonly byte[] _contents;
    protected byte[] _trainer;
    protected byte[] _programRom;
    protected byte[] _characterRom;
    protected int _index;
    protected NesFileOptions _options;

    public string Filename { get; }
    public int ProgramRomSize { get; private set; }
    public int CharacterRomSize { get; private set; }
    public Mapper Mapper { get; protected set; }
    public Flags6 Flags6 { get; private set; }
    public Flags7 Flags7 { get; protected set; }
    public Flags8 Flags8 { get; protected set; }
    public Flags9 Flags9 { get; protected set; }
    public Flags10 Flags10 { get; protected set; }
    public Byte13 Byte13 { get; protected set; }
    public Byte15 Byte15 { get; protected set; }
    public byte[] ProgramRom => _programRom;

    internal ArchaicINesFile(string filename, byte[] contents, NesFileOptions options)
    {
        Filename = filename;
        _contents = contents.ToArray();
        _options = options;
        _trainer = Array.Empty<byte>();
        _programRom = Array.Empty<byte>();
        _characterRom = Array.Empty<byte>();
        _index = 0;

        ParseHeader();
        LoadTrainer();
        LoadProgramRom();
        LoadCharacterRom();
    }

    private void ParseHeader()
    {
        if (! _options.LoadHeader) return;
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

        _index += 4;
    }

    private void LoadProgramSize()
    {
        ProgramRomSize = _contents[_index++];
    }

    private void LoadCharacterSize()
    {
        CharacterRomSize = _contents[_index++];
    }

    private void LoadFlags6()
    {
        Flags6 = new Flags6(_contents[_index++]);
    }

    protected virtual void LoadFlags7()
    {
        Flags7 = new Flags7(0);
        _index++;
    }

    protected virtual void LoadMapperNumber()
    {
        var mapper =  Flags6.LowerMapperNybble;
        Mapper = mapper switch
        {
            <= 32 => new Mapper
            {
                Number = mapper,
                StartAddress = 0xC000,
                EndAddress = 0xFFFF
            },
            _ => throw new ArgumentException($"Cannot load mapper {mapper}"),
        };
    }

    protected virtual void LoadFlags8()
    {
        Flags8 = new Flags8();
        _index++;
    }

    protected virtual void LoadFlags9()
    {
        Flags9 = new Flags9();
        _index++;
    }

    protected virtual void LoadFlags10()
    {
         Flags10 = new Flags10();
         _index++;
    }

    protected virtual void LoadByte11()
    {
        _index++;
    }

    protected virtual void LoadByte12()
    {
        _index++;
    }

    protected virtual void LoadByte13()
    {
         Byte13 = new Byte13();
         _index++;
    }

    protected virtual void LoadByte14()
    {
        _index++;
    }

    protected virtual void LoadByte15()
    {
        _index++;
    }

    protected virtual void LoadTrainer()
    {
        if (!_options.LoadTrainer) return;
        if (Flags6.HasTrainer)
        {
            _trainer = _contents[_index..(_index + TrainerSize)].ToArray();
            _index += TrainerSize;
        }
    }

    protected virtual void LoadProgramRom()
    {
        if (!_options.LoadProgramRom) return;

        var bytes = ProgramRomSize * ProgramRomBlockSize;
        _programRom = _contents[_index..(_index + bytes)].ToArray();
        _index += bytes;
    }

    protected virtual void LoadCharacterRom()
    {
        if (!_options.LoadCharacterRom) return;

        var bytes = CharacterRomSize * CharacterRomBlockSize;
        _characterRom = _contents[_index..(_index + bytes)].ToArray();
        _index += bytes;
    }

    public override string ToString() =>
        $"""
            Filename          : {Path.GetFileName(Filename)}
            File format       : {GetType().Name}
            Program ROM size  : {ProgramRomSize} blocks ({ProgramRomSize * ProgramRomBlockSize} bytes)
            Character ROM size: {CharacterRomSize} blocks ({CharacterRomSize * CharacterRomBlockSize} bytes)
            Mapper number     : {Mapper.Number}
            Flags 6           : {Flags6}
        """;
}