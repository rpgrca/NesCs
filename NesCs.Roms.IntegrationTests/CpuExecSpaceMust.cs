using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Cpu.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class CpuExecSpaceMust
{
    [Theory]
    [InlineData("cpu_exec_space/test_cpu_exec_space_ppuio.nes", 0xE7F5, "\u001b[0;37mTEST:test_cpu_exec_space_ppuio\n\u001b[0;33mThis program verifies that the\nCPU can execute code from any\npossible location that it can\naddress, including I/O space.\n\nIn addition, it will be tested\nthat an RTS instruction does a\ndummy read of the byte that\nimmediately follows the\ninstructions.\n\n\u001b[0;37m\u001b[1;34mJSR+RTS TEST OK\nJMP+RTS TEST OK\nRTS+RTS TEST OK\nJMP+RTI TEST OK\nJMP+BRK TEST OK\n\u001b[0;37m\nPassed\n")] // ticks: 3293317
    [InlineData("cpu_exec_space/test_cpu_exec_space_apu.nes", 0xE976, "", Skip = "Must implement APU, landed at $0234, fail to obey predetermined path #2")]
    public void BeExecutedCorrectly(string romName, int poweroffAddress, string expectedResult)
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
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).Build();
        ramController.RegisterHook(ppu);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .WithClock(clock)
            .WithClockDivisorOf(1)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}