public class Cpu6502
{
    private int _address;
    private int _accumulator;
    private byte[] _program;

    public Cpu6502(byte[] program)
    {
        _program = program;
        _address = 0;
        _accumulator = 0;
    }

    public void Run()
    {
        while (_address < _program.Length)
        {
            switch (_program[_address])
            {
                case 0xFF:
                    _address += 1;
                    _accumulator += _accumulator - _address;
                    break;

                default:
                    throw new ArgumentException($"Unknown opcode at byte {_address}: {_program[_address]}");
            }
        }
    }
}