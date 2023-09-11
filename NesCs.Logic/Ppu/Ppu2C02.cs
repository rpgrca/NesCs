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
    private readonly OamSprite[] _secondaryOam;
    private readonly IByteToggle _toggle;
    private readonly IPpuIOBus _ioBus;
    private bool _odd;
    private byte _nametableLatch;
    private byte _attributeLatch;

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

        _secondaryOam = new OamSprite[8];
        _toggle = new ByteToggle();
        _ioBus = new PpuIOBus(clock);
        clock.AddPpu(this);
        _odd = false;

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
        switch (0x2000 + (index % 8))
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
        return (0x2000 + (index % 8)) switch
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

    public bool CanHandle(int index)
    {
        return index >= 0x2000 && index <= 0x3FFF;
    }

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

    void IPpuVram.Write(byte value) => _vram[CurrentAddress % 0x4000] = value;

    byte IPpuVram.Read() => _vram[CurrentAddress % 0x4000];

    private int _cycle;
//    private StringBuilder _line = new StringBuilder();

    private IPpuLineStep[] _timing = new IPpuLineStep[]
    {
        new LoadNametableByteCycle1(),
        new LoadNametableByteCycle2(),
        new LoadAttributeByteCycle1(),
        new LoadAttributeByteCycle2(),
        new LoadLowBackgroundTileByteCycle1(),
        new LoadLowBackgroundTileByteCycle2(),
        new LoadHighBackgroundTileByteCycle1(),
        new LoadHighBackgroundTileByteCycle2()
    };
/*
    public bool Trigger(IClock clock)
    {
        if (clock.GetCycles() % MasterClockDivisor == 0)
        {
            if (Raster.X == 0)
            {
                if (_odd)
                {
                    _timing[1].Work(this);
                }
                else
                {
                    // idle
                }

                ClearCurrentOamByte();
                Raster.IncrementX();
                return true;
            }
            else if (Raster.X == 1)
            {
                if (Raster.Y == 241)
                {
                    PpuStatus.V = 1;
                    _cycle = clock.GetCycles();
                }
                else if (Raster.Y == 261)
                {
                    PpuStatus.V = 0;
                    PpuStatus.S = 0;
                    PpuStatus.O = 0;
                    _cycle = clock.GetCycles() - _cycle;
                }

                ClearCurrentOamByte();
                _timing[0].Work(this);
                Raster.IncrementX();
                return true;
            }
            else if (Raster.X >= 2 && Raster.X <= 256)
            {
                var timing = _timing[(Raster.X - 1) % 6];
                timing.Work(this);
                Raster.IncrementX();

                if (Raster.X < 63)
                {
                    ClearCurrentOamByte();
                }
                return true;
            }
            else if (Raster.X == 257)
            {
                Raster.IncrementX();
                return true;
            }
            else if (Raster.X >= 258 && Raster.X <= 320)
            {
                // idle
                Raster.IncrementX();
                return true;
            }
            else if (Raster.X >= 321 && Raster.X <= 338)
            {
                var timing = _timing[(Raster.X - 1) % 6];
                timing.Work(this);
                Raster.IncrementX();
                return true;
            }
            else if (Raster.X == 339)
            {
                if (_odd)
                {
                    Raster.BackToOrigin();
                    _odd = false;
                    return true;
                }
                else
                {
                    var timing = _timing[0];
                    timing.Work(this);
                    Raster.IncrementX();
                    return true;
                }
            }
            else if (Raster.X == 340)
            {
                var timing = _timing[1];
                timing.Work(this);
                _odd = true;
                Raster.IncrementX();
                return true;
            }
        }

        return false;
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

    void IPpuTiming.LoadNametableByte()
    {
    }

    void IPpuTiming.LoadHighBackgroundTileByte()
    {
    }

    void IPpuTiming.LoadLowBackgroundTileByte()
    {
    }

    void IPpuTiming.LoadAttributeByte()
    {
    }

    public int CurrentAddress => PpuAddr.CurrentAddress;

    public int MasterClockDivisor => 4;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"PPU: {Raster.Y,3},{Raster.X,3}";
}

public interface IPpuLineStep
{
    void Work(IPpuTiming ppu);
}

public class Idle : IPpuLineStep
{
    public void Work(IPpuTiming ppu)
    {
        // Nothing
    }
}

public class Skip : IPpuLineStep
{
    public void Work(IPpuTiming ppu) => ppu.LoadNametableByte();
}

public class LoadNametableByteCycle1 : IPpuLineStep
{
    public void Work(IPpuTiming ppu)
    {
    }
}

public class LoadNametableByteCycle2 : IPpuLineStep
{
    public void Work(IPpuTiming ppu) => ppu.LoadNametableByte();
}

public class LoadAttributeByteCycle1 : IPpuLineStep
{
    public void Work(IPpuTiming ppu)
    {
    }
}

public class LoadAttributeByteCycle2 : IPpuLineStep
{
    public void Work(IPpuTiming ppu) => ppu.LoadAttributeByte();
}

public class LoadLowBackgroundTileByteCycle1 : IPpuLineStep
{
    public void Work(IPpuTiming ppu)
    {
    }
}

public class LoadLowBackgroundTileByteCycle2 : IPpuLineStep
{
    public void Work(IPpuTiming ppu) => ppu.LoadLowBackgroundTileByte();
}

public class LoadHighBackgroundTileByteCycle1 : IPpuLineStep
{
    public void Work(IPpuTiming ppu)
    {
    }
}

public class LoadHighBackgroundTileByteCycle2 : IPpuLineStep
{
    public void Work(IPpuTiming ppu) => ppu.LoadHighBackgroundTileByte();
}

public class IncrementHorizontal : IPpuLineStep
{
    public void Work(IPpuTiming ppu)
    {
    }
}
