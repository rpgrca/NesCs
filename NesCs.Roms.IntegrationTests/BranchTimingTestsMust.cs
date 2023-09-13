using NesCs.Logic.Cpu;
using NesCs.Logic.Nmi;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;
using NesCs.Logic.Tracing;

namespace NesCs.Roms.IntegrationTests;

public class BranchTimingTestsMust
{
    [Theory]
    [InlineData("branch_timing_tests/1.Branch_Basics.nes", 0xE1B9, "BRANCH TIMING BASICSPASSED")]
    [InlineData("branch_timing_tests/2.Backward_Branch.nes", 0xE17A, "BACKWARD BRANCH TIMINGPASSED")]
    [InlineData("branch_timing_tests/3.Forward_Branch.nes", 0xE17C, "FORWARD BRANCH TIMINGPASSED")]
    public void BeExecutedCorrectly(string romName, int printAddress, string expectedResult)
    {
        var ram = new byte[0x10000];
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/" + romName);
        var clock = new Clock(0);
        var ramController = new RamController.Builder().WithRamOf(ram).PreventRomRewriting().Build();
        var nmiGenerator = new NmiGenerator();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).Build();
        ramController.RegisterHook(ppu);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        builder.ProgramMappedAt(0xC000);

        var message = string.Empty;
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .WithClock(clock)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(0xE4F0, (cpu, _) => cpu.Stop())
            .WithCallback(printAddress, (cpu, _) =>
            {
                message += (char)cpu.ReadByteFromAccumulator();
            })
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        nmiGenerator.AttachTo(cpu);
        cpu.PowerOn();
        cpu.Reset();
        cpu.Run();

        Assert.Equal(expectedResult, message);
    }
}