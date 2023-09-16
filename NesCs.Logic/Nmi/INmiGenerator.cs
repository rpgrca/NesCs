namespace NesCs.Logic.Nmi;

public interface INmiGenerator
{
    void IgnoreVblankThisFrame();
    void SetControl(byte value);
    void SetStatus(byte value);
}