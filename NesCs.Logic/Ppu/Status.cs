using NesCs.Logic.Nmi;
using NesCs.Logic.Ram;

namespace NesCs.Logic.Ppu;

public class Status
{
    private const int StatusIndex = 0x2002;
    private readonly IRamController _ramController;
    private readonly IByteToggle _toggle;
    private readonly IPpuIOBus _ioBus;
    private readonly INmiGenerator _nmiGenerator;
    private readonly RasterAddress _raster;
    private int _forNextVblank;
    private bool _ignoreV;

    private byte Flags
    {
        get
        {
            var value = _ramController.DirectReadFrom(StatusIndex);
            return (byte)((value & 0b11100000) | OpenBus);
        }
        set => _ramController.DirectWriteTo(StatusIndex, value);
    }

    public Status(IRamController ramController, IByteToggle toggle, IPpuIOBus ioBus, INmiGenerator nmiGenerator, RasterAddress raster)
    {
        _ramController = ramController;
        _toggle = toggle;
        _ioBus = ioBus;
        _nmiGenerator = nmiGenerator;
        _raster = raster;
        _forNextVblank = 100;
    }

    public byte OpenBus
    {
        get => (byte)(_ioBus.Read() & 0b11111);
    }

    public byte O
    {
        get
        {
            _toggle.Reset();
            return (byte)((Flags >> 5) & 1);
        }

        set
        {
            Flags = (byte)(Flags & ~(1 << 5));
            Flags |= (byte)((value & 1) << 5);
        }
    }

    public byte S
    {
        get
        {
            _toggle.Reset();
            return (byte)((Flags >> 6) & 1);
        }

        set
        {
            Flags = (byte)(Flags & ~(1 << 6));
            Flags |= (byte)((value & 1) << 6);
        }
    }

    public byte V
    {
        get
        {
            // https://www.nesdev.org/wiki/PPU_registers#PPUSTATUS
            // Reading PPUSTATUS will return the current state of this flag and then clear it.
            _toggle.Reset();
            var result = ObtainVFromFlags();
            ClearVFromFlags();
            _nmiGenerator.SetStatus(0);
            return result;
        }
        set
        {
            if (! _ignoreV)
            {
                ClearVFromFlags();
                SetValueToVFlag(value);
                _nmiGenerator.SetStatus(value);
            }
        }
    }

    private byte ObtainVFromFlags() => (byte)((Flags >> 7) & 1);

    private void ClearVFromFlags() => Flags = ClearVFrom(Flags);

    private void SetValueToVFlag(byte value) => Flags |= (byte)((value & 1) << 7);

    public void Write(byte value) => _ioBus.Write(value);

    public byte Read()
    {
        _toggle.Reset();
        var result = Flags;

        if (_forNextVblank == 3)
        {
            result = ClearVFrom(result);
            _nmiGenerator.SetStatus(0);
            _nmiGenerator.IgnoreVblankThisFrame();
            _ignoreV = true;
        }
        else if (_forNextVblank == 2 || _forNextVblank == 1)
        {
            result = ObtainFlagsWithVSetFrom(result);
            _nmiGenerator.SetStatus(0);
            _nmiGenerator.CancelInterrupt();
        }
        else
        {
            V = 0;
        }

        return result;
    }

    private byte ClearVFrom(byte flags) => (byte)(flags & ~(1 << 7));

    private byte ObtainFlagsWithVSetFrom(byte flags) => (byte)(flags | (1 << 7));

    internal void ResetIgnoreV() => _ignoreV = false;

    internal void ResetTimerForNextVblank() => _forNextVblank = (241 * 341) + 1;

    internal void DecrementTimerForNextVblank() => _forNextVblank--;
}