namespace NesCs.Logic.Nmi;

public interface INmiGenerator
{
    void SetControl(byte value);
    void SetStatus(byte value);
}