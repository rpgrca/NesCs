namespace NesCs.Logic.Nmi;

public class DummyNmiGenerator : INmiGenerator
{
    public void CancelInterrupt()
    {
    }

    public void IgnoreVblankThisFrame()
    {
    }

    public void SetControl(byte value)
    {
    }

    public void SetStatus(byte value)
    {
    }
}