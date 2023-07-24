if (args.Length < 1)
{
    Console.WriteLine($"Usage: program <filename>");
    return 1;
}

Console.WriteLine($"Loading {args[0]}...");

var fsp = FileSystemProxy.Create();
var nesFile = fsp.Load(args[0]);

Console.WriteLine($"Loaded!");
Console.WriteLine(nesFile.ToString());
return 0;