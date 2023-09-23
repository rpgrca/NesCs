using NesCs.Logic.Cpu;

namespace NesCs.Logic.Nmi;

public interface INmiGenerator
{
    void IgnoreVblankThisFrame();
    void SetControl(byte value);
    void SetStatus(byte value);
    void CancelInterrupt();
    void AttachTo(Cpu6502 cpu);
}