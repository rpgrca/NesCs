﻿namespace NesCs.Logic;

public class NesFile : INesFile
{
    private static readonly byte[] HeaderSignature = new byte[] { 0x4e, 0x45, 0x53, 0x1A };
    private const int HeaderSignatureIndex = 0;
    private const int HeaderProgramSizeIndex = 4;
    private const int HeaderCharacterSizeIndex = 5;

    private readonly byte[] _contents;

    public string Filename { get; }
    public int ProgramRomSize { get; private set; }
    public int CharacterRomSize { get; private set; }

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
}