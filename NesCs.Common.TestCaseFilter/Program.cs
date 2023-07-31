if (args.Length < 1)
{
    Console.WriteLine($"Usage: program filename");
    return 1;
}

var filename = args[0];
var opcode = System.IO.Path.GetFileNameWithoutExtension(filename).ToUpper();

var filter = new NesCs.Common.TestCaseFilter.ProcessorFileFilter(filename);
var cases = filter.FilterUniqueCases();

var formatter = new NesCs.Common.TestCaseFilter.TestCaseFormatter(opcode, cases);
Console.WriteLine(formatter.ToString());

return 0;