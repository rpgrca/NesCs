using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Ppu2C02 : IPpu
{
    public class Builder
    {
        private byte[]? _vram;
        private IRamController? _ramController;

        public Builder WithVram(byte[] vram)
        {
            _vram = vram;
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
            _ramController ??= new RamController.Builder().Build();

            return new Ppu2C02(_ramController, _vram);
        }
    }

    private readonly byte[] _vram;
    private readonly OamSprite[] _oam;
    private readonly OamSprite[] _secondaryOam;
    private readonly IByteToggle _toggle;
    
    public ControlRegister PpuCtrl { get; } /* 0x2000 */
    public Mask PpuMask { get; }            /* 0x2001 */
    public Status PpuStatus { get; }        /* 0x2002 */
    public OamAddressPort OamAddr { get; }  /* 0x2003 */
    public OamDataPort OamData { get; }     /* 0x2004 */
    public ScrollingPositionRegister PpuScroll { get; }        /* 0x2005 */
    public AddressRegister PpuAddr { get; } /* 0x2006 */
    public DataPort PpuData { get; }        /* 0x2007 */
    public OamDmaRegister OamDma { get; }   /* 0x4014 */

    private Ppu2C02(IRamController ram, byte[] vram)
    {
        _vram = vram;
        _oam = new OamSprite[64];
        _secondaryOam = new OamSprite[8];
        _toggle = new ByteToggle();

        PpuCtrl = new ControlRegister(ram);
        PpuMask = new Mask(ram);
        PpuStatus = new Status(ram);
        OamAddr = new OamAddressPort(ram);
        OamData = new OamDataPort(OamAddr);
        PpuScroll = new ScrollingPositionRegister(ram, _toggle);
        PpuAddr = new AddressRegister(ram, _toggle);
        PpuData = new DataPort(this);
        OamDma = new OamDmaRegister();
    }

    void IRamHook.Write(int index, byte value)
    {
        switch (index)
        {
            case 0x2000: PpuCtrl.Write(value, this); break;
            case 0x2001: PpuMask.Write(value, this); break;
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

    public int CurrentAddress => PpuAddr.CurrentAddress;
}

public class ByteToggle : IByteToggle
{
    private int _toggle;

    public int GetIndex()
    {
        var result = _toggle;
        _toggle = (_toggle + 1) & 1;
        return result;
    }
}