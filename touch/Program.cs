using System.ComponentModel.Design;
using CLIToolsCommon;

if(args.Length == 0)
{
    Usage();
    return 0;
}

CommandLineArguments cm;
try
{
    Dictionary<char,string> validArguments = new(){{'h',"help"}};
    cm = new(args,validArguments);
} catch(Exception ex)
{
    Console.WriteLine(ex);
    return 1;
}

if(cm.Arguments.Contains("help"))
{
    Help();
    return 0;
}

int error = 0;

foreach(string path in cm.Paths) {
    if(File.Exists(path))
    {
        Console.WriteLine($"File {path} allready Exisit");
        error = 1;
        continue;
    }

    try
    {
        File.Create(path);
    } catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        error = 2;
        continue;
    }
}

return error;


static void Usage()
{
        Console.WriteLine("Usage: touch [--help|-h] filenames");
}

static void Help()
{
    Console.WriteLine("This program creates new files");
    Usage();
    Console.WriteLine("-h --help\tPrint this");  
}