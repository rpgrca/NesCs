namespace NesCs.Logic.Nmi;

public class DummyNmiGenerator : INmiGenerator
{
    public bool Generated => false;

    public void Clear()
    {
    }

    public void IgnoreVblankThisFrame()
    {
    }

    public void Set()
    {
    }

    public void SetControl(byte value)
    {
    }

    public void SetStatus(byte value)
    {
    }
}