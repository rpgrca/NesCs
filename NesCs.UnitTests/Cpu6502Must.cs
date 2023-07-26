using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using NesCs.Logic.File;
using System.Text.Json;

namespace NesCs.UnitTests;


public class Cpu6502Must
{

    [Fact(Skip = "Sample test for future implementation")]
    public void FutureTest()
    {
        var nesFile = CreateNesFile("1.Branch_Basics.nes");
        var sut = new Cpu6502(nesFile.ProgramRom, nesFile.Mapper.StartAddress, nesFile.Mapper.EndAddress);
        sut.Run();
    }

    private static INesFile CreateNesFile(string romName) =>
        new FileSystemProxy.Builder()
            .AccessingWith(CreateFileSystemWithRom(romName))
            .Build()
            .Load(romName);

    private static IFileSystem CreateFileSystemWithRom(string romName) =>
        new MockFileSystem(new Dictionary<string, MockFileData>() {
            { romName, new(LoadRom(romName)) }
        });

    private static byte[] LoadRom(string name)
    {
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        var file = embeddedProvider.GetFileInfo($"Resources/{name}");
        byte[] array = new byte[file.Length];

        using var stream = file.CreateReadStream();
        stream.Read(array, 0, (int)file.Length);
        return array;
    }
}
