using System.Diagnostics;
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
            _toggle.Reset();
            var result = (byte)((Flags >> 7) & 1);
            V = 0;
            return result;
        }
        set
        {
            Flags = (byte)((Flags & ~(1 << 7)) | ((value & 1) << 7));
            _nmiGenerator.SetStatus(value);
        }
    }

    public void Write(byte value)
    {
        _ioBus.Write(value);
    }

    public byte Read()
    {
        _toggle.Reset();
        var result = Flags;
        V = 0;

        if (_forNextVblank == 1)
        {
            result = (byte)(result & ~(1 << 7));
            _nmiGenerator.SetStatus(0);
            _nmiGenerator.IgnoreVblankThisFrame();
        }
        else if (_forNextVblank == 0)
        {
            result = (byte)(result & ~(1 << 7));
            _nmiGenerator.SetStatus(0);
            _nmiGenerator.IgnoreVblankThisFrame();
        }
        else if (_forNextVblank == -1)
        {
            _nmiGenerator.SetStatus(0);
            _nmiGenerator.IgnoreVblankThisFrame();
        }

        _forNextVblank = 100;
        return result;
    }

    internal void ForNextVblank(int v)
    {
        _forNextVblank = v;
    }
}