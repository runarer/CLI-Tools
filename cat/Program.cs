using CLIToolsCommon;

// If no arguments, display usage and exit
if (args.Length < 1)
{
    Usage();
    return 0;
}

// Parse commandline input
CommandLineArguments cm;
bool number = false;
bool numberNonBlanks = false;
bool showEnds = false;
bool squeezeBlank = false;
int lineNumber = 0;

try
{
    Dictionary<char,string> validArguments = new(){ {'n',"number"},{'E',"show-ends"},{'B',"number-nonblank"},{'h',"help"},{'s',"squeeze-blank"}};
    cm = new(args,validArguments);
    
    if(cm.Arguments.Contains("help") )
    {  
        Help();
        return 0;
    }

    if(cm.Arguments.Contains("number"))
        number = true;

    if(cm.Arguments.Contains("number-nonblank"))
    {        
        numberNonBlanks = true;
        number = true;
    }    
    if(cm.Arguments.Contains("show-ends"))
        showEnds = true;
    if(cm.Arguments.Contains("squeeze-blank"))
        squeezeBlank = true;

} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return 1;
}

// Read and print all files
foreach(string path in cm.Paths)
{
    string[] lines;
    try
    {
        lines = File.ReadAllLines(path);
    } catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        return 2;
    }
    
    bool lastEmpty = false; // To keep track of last line being empty for squeeze blank
    foreach(string line in lines) {

        // Squeeze blank lines
        if(squeezeBlank && line == "")
            if(lastEmpty)
                continue;
            else
                lastEmpty = true;
        else
            lastEmpty = false;

        // Number lines
        if(number)
            if((numberNonBlanks && line != "") || !numberNonBlanks)            
                Console.Write($"{lineNumber++,-4}");

        
        // Print line and add $ if show ends
        Console.Write(line);
        if(showEnds)
            Console.Write('$');
        Console.WriteLine();
    }
}

return 0;

static void Usage()
{
    Console.WriteLine("Usage: cat [-hnBEs] [--argument] filename");
}

static void Help()
{
    Usage();
    Console.WriteLine("-n --number\t-\tNumber line");
    Console.WriteLine("-E --show-ends\t-\t$ added to end of line");
    Console.WriteLine("-B --number-nonblank\t-\tOnly number non empty lines");
    Console.WriteLine("-s --squeeze-blank\t-\tSuspend newline");
    Console.WriteLine("-h --help\t-\tShow this");
}