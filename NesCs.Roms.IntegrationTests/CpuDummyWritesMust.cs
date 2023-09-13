using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Cpu.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class CpuDummyWritesMust
{
    [Theory]
    [InlineData("cpu_dummy_writes/cpu_dummy_writes_ppumem.nes", 0xE815, 100_000_000, "\u001b[0;37mTEST: cpu_dummy_writes_ppumem\n\u001b[0;33mThis program verifies that the\nCPU does 2x writes properly.\nAny read-modify-write opcode\nshould first write the origi-\nnal value; then the calculated\nvalue exactly 1 cycle later.\n\n\u001b[0;37mVerifying open bus behavior.\n\u001b[0;33m      W- W- WR W- W- W- W- WR\n2000+ 0  1  2  3  4  5  6  7 \n\u001b[0;33m  R0:\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m\n\u001b[0;33m  R1:\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m\n\u001b[0;33m  R3:\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m\n\u001b[0;33m  R5:\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m\n\u001b[0;33m  R6:\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m- 0\u001b[1;34m0\u001b[1;34m\n\u001b[0;37mOK; \u001b[0;37mVerifying opcodes...\n\u001b[1;34m0E\u001b[1;34m2E\u001b[1;34m4E\u001b[1;34m6E\u001b[1;34mCE\u001b[1;34mEE \u001b[1;34m1E\u001b[1;34m3E\u001b[1;34m5E\u001b[1;34m7E\u001b[1;34mDE\u001b[1;34mFE \n\u001b[1;34m0F\u001b[1;34m2F\u001b[1;34m4F\u001b[1;34m6F\u001b[1;34mCF\u001b[1;34mEF \u001b[1;34m1F\u001b[1;34m3F\u001b[1;34m5F\u001b[1;34m7F\u001b[1;34mDF\u001b[1;34mFF \n\u001b[1;34m03\u001b[1;34m23\u001b[1;34m43\u001b[1;34m63\u001b[1;34mC3\u001b[1;34mE3 \u001b[1;34m13\u001b[1;34m33\u001b[1;34m53\u001b[1;34m73\u001b[1;34mD3\u001b[1;34mF3 \n\u001b[1;34m1B\u001b[1;34m3B\u001b[1;34m5B\u001b[1;34m7B\u001b[1;34mDB\u001b[1;34mFB              \n\u001b[0;37m\nPassed\n")] // ticks: 8_773_381  77_879_701
    [InlineData("cpu_dummy_writes/cpu_dummy_writes_oam.nes", 0xE815, 200_000_000, "\u001b[0;37mTEST: cpu_dummy_writes_oam\n\u001b[0;33mThis program verifies that the\nCPU does 2x writes properly.\nAny read-modify-write opcode\nshould first write the origi-\nnal value; then the calculated\nvalue exactly 1 cycle later.\n\nRequirement: OAM memory reads\nMUST be reliable. This is\noften the case on emulators,\nbut NOT on the real NES.\nNevertheless, this test can be\nused to see if the CPU in the\nemulator is built properly.\n\n\u001b[0;37m\u001b[0;37mTesting OAM.  The screen will go blank for a moment now.\n\u001b[0;37mOK; \u001b[0;37mVerifying opcodes...\n\u001b[1;34m0E\u001b[1;34m2E\u001b[1;34m4E\u001b[1;34m6E\u001b[1;34mCE\u001b[1;34mEE \u001b[1;34m1E\u001b[1;34m3E\u001b[1;34m5E\u001b[1;34m7E\u001b[1;34mDE\u001b[1;34mFE \n\u001b[1;34m0F\u001b[1;34m2F\u001b[1;34m4F\u001b[1;34m6F\u001b[1;34mCF\u001b[1;34mEF \u001b[1;34m1F\u001b[1;34m3F\u001b[1;34m5F\u001b[1;34m7F\u001b[1;34mDF\u001b[1;34mFF \n\u001b[1;34m03\u001b[1;34m23\u001b[1;34m43\u001b[1;34m63\u001b[1;34mC3\u001b[1;34mE3 \u001b[1;34m13\u001b[1;34m33\u001b[1;34m53\u001b[1;34m73\u001b[1;34mD3\u001b[1;34mF3 \n\u001b[1;34m1B\u001b[1;34m3B\u001b[1;34m5B\u001b[1;34m7B\u001b[1;34mDB\u001b[1;34mFB              \n\u001b[0;37m\nPassed\n")] // ticks 9_319_127 111_829_525
    //[InlineData("cpu_dummy_reads/cpu_dummy_reads.nes", 0x1, 100_000_000, "", Skip = "No output but hangs at 0xE60F which is error in fceux")]
    public void BeExecutedCorrectly(string romName, int poweroffAddress, int maximumTicks, string expectedResult)
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
        var clock = new Clock(0, maximumTicks);
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
        cpu.Reset();
        cpu.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}