using NesCs.Logic;
using NesCs.Logic.Cpu;
using NesCs.Logic.Cpu.Instructions;

public class Vm6502DebuggerDisplay : ITracer
{
    private string _previous = "$0000: $00 $00";
    private string _current = string.Empty;
    private bool _display = false;

    public Vm6502DebuggerDisplay(bool display = false) => _display = display;

    public void Display(IInstruction instruction, byte[] operands, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles)
    {
        if (_display)
        {
            _previous = _current;
            var text = string.Join(" ", operands.Select(p => $"${p:X2}")).PadRight(7);
            var value = operands.Length switch
            {
                1 => $"${operands[0]:X2}  ",
                2 => $"${operands[1]:X2}{operands[0]:X2}",
                _ => "    "
            };
            _current = $"${pc:X4}: ${instruction.Opcode:X2} {text,-5}    {instruction.Name} {value}";
            System.Diagnostics.Debug.Print($"*-----------------*-----------------------*----------*----------*------------*");
            System.Diagnostics.Debug.Print($"|    PC: ${pc:X4}    |  Acc: ${a:X2} ({Convert.ToString(a, 2).PadLeft(8, '0')})  |  X: ${x:X2}  |  Y: ${y:X2}  |  {cycles:D08}  |");
            System.Diagnostics.Debug.Print("*-----------------*-----------------------*----------*----------*------------*");
            System.Diagnostics.Debug.Print($"|  NV-BDIZC       | : {_previous}                         ");
            System.Diagnostics.Debug.Print($"|  {Convert.ToString((byte)p, 2).PadLeft(8, '0')} ({(byte)p:X2})  | : {_current}");
            System.Diagnostics.Debug.Print("*-----------------*\n");
            System.Diagnostics.Debug.Print($"Stack: ${s:X2}\n");
        }
    }

    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}