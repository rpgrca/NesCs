using System.Diagnostics;
using NesCs.Logic.Cpu.Clocking;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Ppu2C02 : IPpu
{
    // TODO: Should only skip, not change cycle amount
    private const int LinesPerSync = 262;

    public class Builder
    {
        private byte[]? _vram;
        private IRamController? _ramController;
        private IClock? _clock;

        public Builder WithVram(byte[] vram)
        {
            _vram = vram;
            return this;
        }

        public Builder WithClock(IClock clock)
        {
            _clock = clock;
            return this;
        }

        public Builder WithRamController(IRamController ramController)
        {
            _ramController = ramController;
            return this;
        }

        public IPpu Build()
        {
            _vram ??= new byte[0x4000];
            _clock ??= new Clock(0);
            _ramController ??= new RamController.Builder().Build();

            return new Ppu2C02(_ramController, _vram, _clock);
        }
    }

    private readonly byte[] _vram;
    private readonly OamSprite[] _oam;
    private readonly OamSprite[] _secondaryOam;
    private readonly IByteToggle _toggle;
    private readonly IPpuIOBus _ioBus;
    private bool _odd;

    private RasterAddress Raster { get; }
    public ControlRegister PpuCtrl { get; }                 /* 0x2000 W  */
    public Mask PpuMask { get; }                            /* 0x2001 W  */
    public Status PpuStatus { get; }                        /* 0x2002  R */
    public OamAddressPort OamAddr { get; }                  /* 0x2003 W  */
    public OamDataPort OamData { get; }                     /* 0x2004 WR */
    public ScrollingPositionRegister PpuScroll { get; }     /* 0x2005 W  */
    public AddressRegister PpuAddr { get; }                 /* 0x2006 W  */
    public DataPort PpuData { get; }                        /* 0x2007 WR */
    public OamDmaRegister OamDma { get; }                   /* 0x4014 W  */

    private Ppu2C02(IRamController ram, byte[] vram, IClock clock)
    {
        _vram = vram;
        _oam = new OamSprite[64];
        _secondaryOam = new OamSprite[8];
        _toggle = new ByteToggle();
        _ioBus = new PpuIOBus(clock);
        clock.AddPpu(this);
        _odd = true;

        Raster = new RasterAddress();
        PpuCtrl = new ControlRegister(ram, _ioBus);
        PpuMask = new Mask(ram, _ioBus);
        PpuStatus = new Status(ram, _toggle, _ioBus);
        OamAddr = new OamAddressPort(ram, _ioBus);
        PpuScroll = new ScrollingPositionRegister(ram, _toggle, _ioBus);
        OamData = new OamDataPort(OamAddr, PpuMask, Raster, _ioBus);
        PpuAddr = new AddressRegister(ram, _toggle, _ioBus);
        PpuData = new DataPort(this, _ioBus);
        OamDma = new OamDmaRegister();
    }

    void IRamHook.Write(int index, byte value)
    {
        switch (index)
        {
            case 0x2000: PpuCtrl.Write(value); break;
            case 0x2001: PpuMask.Write(value); break;
            case 0x2002: PpuStatus.Write(value); break;
            case 0x2003: OamAddr.Write(value); break;
            case 0x2004: OamData.Write(value); break;
            case 0x2005: PpuScroll.Write(value); break;
            case 0x2006: PpuAddr.Write(value); break;
            default: PpuData.Write(value); break;
        }
    }

    byte IRamHook.Read(int index)
    {
        return index switch
        {
            0x2000 => PpuCtrl.Read(),
            0x2001 => PpuMask.Read(),
            0x2002 => PpuStatus.Read(),
            0x2003 => OamAddr.Read(),
            0x2004 => OamData.Read(),
            0x2005 => PpuScroll.Read(),
            0x2006 => PpuAddr.Read(),
            _ => PpuData.Read()
        };
    }

    public bool CanHandle(int index) => index >= 0x2000 && index <= 0x2007;

    public void IncrementAddress()
    {
        if (PpuCtrl.I == 0)
        {
            PpuAddr.IncrementBy(1);
        }
        else
        {
            PpuAddr.IncrementBy(0x20);
        }
    }

    void IPpuVram.Write(byte value) => _vram[CurrentAddress] = value;

    byte IPpuVram.Read() => _vram[CurrentAddress];

    private int _cycle;
//    private StringBuilder _line = new StringBuilder();
/*
    private List<IPpuLineStep>[] _odd = new List<IPpuLineStep>
    {
        new Skip(),
        new LoadNametableByteCycle1(),
        new LoadNametableByteCycle2(),
        new LoadAttributeByteCycle1(),
        new LoadAttributeByteCycle2(),
        new LoadLowBackgroundTileByte(),
        new LoadHighBackgroundTileByteAndIncrementHorizontal()
    }

    private List<IPpuLineStep>[] _even = new()
    {

    }
*/
    public bool Trigger(IClock clock)
    {
//        string value;
        if (clock.GetCycles() % 4 == 0)
        {
            switch (Raster.X)
            {
                case 0:
//                    value = " idl |";

                    if (Raster.Y == 0)
                    {
                        _odd = !_odd;

                        if (!_odd)
                        {
//                            value = " skp |";
                        }
                    }

                    //_line.Append(value);
                    Raster.IncrementX();
                    break;

                case 1:
//                    value = "     |";
                    if (Raster.Y == 241)
                    {
                        PpuStatus.V = 1;
                        _cycle = clock.GetCycles();
//                        value = " +vb |";
                    }
                    else
                    {
                        if (Raster.Y == 261)
                        {
                            PpuStatus.V = 0;
                            PpuStatus.S = 0;
                            PpuStatus.O = 0;
                            _cycle = clock.GetCycles() - _cycle;
//                            value = " -vb |";
                        }
                    }

//                    _line.Append(value);
                    Raster.IncrementX();
                    break;

                case 339:
//                    _line.Append("     |");

                    if (Raster.Y == 261 && _odd)
                    {
                        Raster.ResetX();
                        Raster.ResetY();
//                        _line.Append("\n\n\n");
//                        System.IO.File.AppendAllText("/home/roberto/src/NesCs/ppu.log", _line.ToString());
//                        _line.Clear();
                    }
                    else
                    {
                        Raster.IncrementX();
                    }
                    break;

                case 340:
//                    _line.Append("     |\n");

                    Raster.ResetX();
                    Raster.IncrementY();
                    if (Raster.Y >= LinesPerSync)
                    {
                        Raster.ResetY();
//                        _line.Append("\n\n\n");
                    }

//                    System.IO.File.AppendAllText("/home/roberto/src/NesCs/ppu.log", _line.ToString());
//                    _line.Clear();
                    break;

                default:
//                    _line.Append("     |");
                    Raster.IncrementX();
                    break;
            }

            return true;


//                // On an NTSC machine, the VBL flag is cleared 6820 ppu cycles or exactly 20 scanlines after it is set.
//                if (Raster.Y == 20 && Raster.X == 0)
//                {
//                    PpuStatus.V = 0;
//                }
//                else
//                /* V set at 240?
//                if (_rasterY == 240)
//                {
//                    // CPU should be at around 29658
//                    //PpuStatus.V = 1;
//                }
//                else*/
//                {
//                    if (Raster.Y >= LinesPerSync)
//                    {
//                        PpuStatus.V = 1;
//                        Raster.ResetY();
//                        _currentCycle = (_currentCycle + 1) % 2;
//                    }
//                }
            //}

            return true;
        }

        return false;
    }

    public string GetStatus() => DebuggerDisplay;

    public int CurrentAddress => PpuAddr.CurrentAddress;

    public int MasterClockDivisor => 4;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"PPU: {Raster.Y,3},{Raster.X,3}";
}

public interface IPpuLineStep
{
    void Work(Ppu2C02 ppu);
}

public class Idle : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
    }
}

public class Skip : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
    }
}

public class LoadNametableByteCycle1 : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
    }
}

public class LoadNametableByteCycle2 : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
    }
}

public class LoadAttributeByteCycle1 : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
    }
}

public class LoadAttributeByteCycle2 : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
    }
}

public class LoadLowBackgroundTileByte : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
        throw new NotImplementedException();
    }
}

public class LoadHighBackgroundTileByteAndIncrementHorizontal : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
        throw new NotImplementedException();
    }
}

public class IncrementHorizontal : IPpuLineStep
{
    public void Work(Ppu2C02 ppu)
    {
        throw new NotImplementedException();
    }
}
