using NesCs.Logic;
using NesCs.Logic.Cpu;

public class Vm6502DebuggerDisplay : ITracer
{
    private string _previous = "$0000: $00 $00";
    private string _current = "$0000: $00 $00";

    public void Display(byte opcode, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles)
    {
        _previous = _current;
        _current = $"${pc:X4}: ${opcode:X2}        ";
        System.Diagnostics.Debug.Print($"*-----------------*-----------------------*----------*----------*------------*");
        System.Diagnostics.Debug.Print($"|    PC: ${pc:X4}    |  Acc: ${a:X2} ({Convert.ToString(a, 2).PadLeft(8, '0')})  |  X: ${x:X2}  |  Y: ${y:X2}  |  {cycles:D08}  |");
        System.Diagnostics.Debug.Print("*-----------------*-----------------------*----------*----------*------------*");
        System.Diagnostics.Debug.Print($"|  NV-BDIZC       | : {_previous}       XXX #$00          ");
        System.Diagnostics.Debug.Print($"|  {Convert.ToString((byte)p, 2).PadLeft(8, '0')} ({(byte)p:X2})  | : {_current}");
        System.Diagnostics.Debug.Print("*-----------------*\n");
        System.Diagnostics.Debug.Print($"Stack: ${s:X2}\n");
    }

    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}