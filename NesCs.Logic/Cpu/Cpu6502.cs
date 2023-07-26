public class Cpu6502
{
    private int _ip;
    private byte[] _program;

    public Cpu6502(byte[] program)
    {
        _program = program;
    }

    public void Run()
    {
        _ip = 0;
        while (_ip < 10)
        {
            var opcode = _program[_ip];
        }
    }
}