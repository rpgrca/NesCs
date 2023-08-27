using System.Diagnostics;
using NesCs.Logic.Cpu;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Ppu2C02 : IPpu
{
    private const int EvenCycle = 341;
    private const int OddCycle = 340;
    private const int LinesPerSync = 262;
    private static readonly int[] _cyclesPerLine = { EvenCycle, OddCycle };

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
    private int _rasterX, _rasterY;
    private int _currentCycle;
    
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
        _currentCycle = 0;
        _rasterX = _rasterY = 0;

        PpuCtrl = new ControlRegister(ram, _ioBus);
        PpuMask = new Mask(ram, _ioBus);
        PpuStatus = new Status(ram, _toggle, _ioBus);
        OamAddr = new OamAddressPort(ram, _ioBus);
        OamData = new OamDataPort(OamAddr, _ioBus);
        PpuScroll = new ScrollingPositionRegister(ram, _toggle, _ioBus);
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

    public bool Trigger(int tick)
    {
        if ((tick) % 4 == 0)
        {
            _rasterX += 1;

            if (_rasterX >= _cyclesPerLine[_currentCycle])
            {
                _rasterX = 0;
                _rasterY += 1;
                if (_rasterY >= LinesPerSync)
                {
                    _rasterY = 0;
                    _currentCycle = (_currentCycle + 1) % 2;
                }
            }

            return true;
        }

        return false;
    }

    public string GetStatus() => DebuggerDisplay;

    public int CurrentAddress => PpuAddr.CurrentAddress;

    public int MasterClockDivisor => 4;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"PPU: {_rasterY,3},{_rasterX,3}";
}