
using CLIToolsCommon;

bool physical = false;

if(args.Length > 0)
{
    Dictionary<char,string> validArguments = new(){ {'L',"logical"},{'P',"physical"},{'h',"help"},{'v',"version"}};
    CommandLineArguments cm;
    try{
        cm = new(args, validArguments);
    } catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return 1;
    }

    if(cm.Arguments.Contains("help")){
        Help();
        return 0;
    }
    if(cm.Arguments.Contains("version")) {
        Version();
        return 0;
    }
    if(cm.Arguments.Contains("physical"))
        physical = false;
    // Logical is default
}

string dir = Directory.GetCurrentDirectory();

if(physical) {
    FileSystemInfo?  fileInfo = Directory.ResolveLinkTarget(dir,physical);

    if(fileInfo == null)
    {
        Console.WriteLine("Could not resolve link.");
        return 2;
    }
    dir = fileInfo.FullName;
}

Console.WriteLine(dir);
return 0;

static void Version()
{
    // Can I get version number from project, 
    // and how to add version to project,
    // and this code be added to common code, but use version for this project. 
    Console.WriteLine("Version 0.1 alpha");
}

static void Help()
{
    Console.WriteLine("Usage: pwd [-L|P|h|v] [--argument]");
    Console.WriteLine("Valid Arguments");
    Console.WriteLine("L - logical\t-\tShow logical location (Default)");
    Console.WriteLine("P -physical\t-\tShow physical location");
    Console.WriteLine("h - help\t-\tDisplay this text.");
    Console.WriteLine("v - version\t-\tSuspend newline");
}