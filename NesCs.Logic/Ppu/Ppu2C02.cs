using System.Diagnostics;
using NesCs.Logic.Nmi;
using NesCs.Logic.Clocking;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Ppu2C02 : IPpu
{
    private const int LinesPerSync = 262;

    public class Builder
    {
        private byte[]? _vram;
        private IRamController? _ramController;
        private IClock? _clock;
        private INmiGenerator? _nmiGenerator;
        private RasterAddress? _rasterAddress;

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

        public Builder WithRaster(RasterAddress rasterAddress)
        {
            _rasterAddress = rasterAddress;
            return this;
        }

        public Builder WithNmiGenerator(INmiGenerator nmiGenerator)
        {
            _nmiGenerator = nmiGenerator;
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
            _rasterAddress ??= new RasterAddress();
            _nmiGenerator ??= new DummyNmiGenerator();
            _ramController ??= new RamController.Builder().Build();

            return new Ppu2C02(_ramController, _vram, _clock, _nmiGenerator, _rasterAddress);
        }
    }

    private readonly byte[] _vram;
    private readonly OamSprite[] _secondaryOam;
    private readonly IByteToggle _toggle;
    private readonly IPpuIOBus _ioBus;
    private bool _odd;
    private byte _nametableLatch;
    private byte _attributeLatch;
    private readonly INmiGenerator _nmiGenerator;

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

    private Ppu2C02(IRamController ram, byte[] vram, IClock clock, INmiGenerator nmiGenerator, RasterAddress rasterAddress)
    {
        _vram = vram;

        _secondaryOam = new OamSprite[8];
        _toggle = new ByteToggle();
        _ioBus = new PpuIOBus(clock);
        clock.AddPpu(this);
        _odd = false;
        _nmiGenerator = nmiGenerator;

        Raster = rasterAddress;
        PpuCtrl = new ControlRegister(ram, _ioBus, _nmiGenerator);
        PpuMask = new Mask(ram, _ioBus);
        PpuStatus = new Status(ram, _toggle, _ioBus, _nmiGenerator, Raster);
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

    void IPpuVram.Write(byte value)
    {
        if (CurrentAddress >= 0x3F00)
        {
            // TODO: Optimize with single palette
            var offset = (CurrentAddress - 0x3F00) & 0x0F;
            _vram[0x3F00 + offset] = _vram[0x3F10 + offset] = _vram[0x3F20 + offset] =
            _vram[0x3F30 + offset] = _vram[0x3F40 + offset] = _vram[0x3F50 + offset] =
            _vram[0x3F60 + offset] = _vram[0x3F70 + offset] = _vram[0x3F80 + offset] =
            _vram[0x3F90 + offset] = _vram[0x3FA0 + offset] = _vram[0x3FB0 + offset] =
            _vram[0x3FC0 + offset] = _vram[0x3FD0 + offset] = _vram[0x3FE0 + offset] =
            _vram[0x3FF0 + offset] = value;
        }
        else
        {
            _vram[CurrentAddress] = value;
        }
    }

    byte IPpuVram.Read() => _vram[CurrentAddress % 0x4000];

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

    public bool Trigger(IClock clock)
    {
        if (clock.GetCycles() % MasterClockDivisor == 0)
        {
            PpuStatus.DecrementTimerForNextVblank();
            switch (Raster.X)
            {
                case 0:
                    if (Raster.Y == 0)
                    {
                        _odd = !_odd;
                        PpuStatus.ResetIgnoreV();
                        PpuStatus.ResetTimerForNextVblank();
                    }

                    Raster.IncrementX();
                    break;

                case 1:
                    if (Raster.Y == 241)
                    {
                        PpuStatus.V = 1;
                    }
                    else
                    {
                        if (Raster.Y == 261)
                        {
                            PpuStatus.V = 0;
                            PpuStatus.S = 0;
                            PpuStatus.O = 0;
                        }
                    }
    
                    Raster.IncrementX();
                    break;
    
                case 339:
                    if (Raster.Y == 261)
                    {
                        if (_odd && (PpuMask.Lb + PpuMask.Ls > 0))
                        {
                            Raster.BackToOrigin();
                        }
                        else
                        {
                            Raster.IncrementX();
                        }
                    }
                    else
                    {
                        Raster.IncrementX();
                    }
                    break;
    
                case 340:
                    if (Raster.Y >= 261)
                    {
                        Raster.BackToOrigin();
                    }
                    else
                    {
                        Raster.IncrementY();
                        Raster.ResetX();
                    }
                    break;
    
                default:
                    Raster.IncrementX();
                    break;
            }
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