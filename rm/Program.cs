using System.Linq;
using System.Text;

Dictionary<char,string> d1 = new(){ {'h',"hei"},{'d',"deg"},{'D',"de"},{'m',"meg"}};
CommandLineArguments cm;
try {
    cm = CommandLineArguments.ParseArguments(args,d1);
} catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return 2;
}

if(cm.Paths.Length < 1 )
{
    PrintUsage();
    return 0;
}

var content = Directory.EnumerateFileSystemEntries(cm.Paths[0],cm.SearchPatterns[0],SearchOption.TopDirectoryOnly);
DeleteListed(content);

return 0;

static void DeleteListed(IEnumerable<string> list)
{
    // list.Reverse();
    foreach(string s in Enumerable.Reverse(list) )
    {
        Console.WriteLine(s);
    }
}

static void PrintUsage()
{
    Console.WriteLine($"Usage: {System.Diagnostics.Process.GetCurrentProcess().ProcessName} <arguments> <searchPattern> <filenames ..>");
}

class CommandLineArguments
{
    public string[] Paths {private set; get; } = [];
    public string[] SearchPatterns {private set; get; } = [];
    public string[] Arguments {private set; get; } = [];


    static public CommandLineArguments ParseArguments(string[] args, Dictionary<char,string> _attributes)
    {
        HashSet<string> files = [];
        HashSet<string> searchPatterns = [];
        HashSet<string> arguments = [];

        CommandLineArguments cm = new();
        if(args.Length < 1)
            return cm;

        foreach(string arg in args)
        {
            if(arg.StartsWith("--"))
            {
                if(arg.Length == 2)
                    throw new Exception("-- lacks an argument");
                if(_attributes.Values.Contains(arg[2..]))
                {
                    if(arguments.Contains(arg[2..])) {
                        char value = _attributes.First( v => v.Value == arg).Key;
                        throw new Exception($"Argument {arg} already used, another --{arg} or  -{value} ");
                    }
                   arguments.Add(arg[2..]); 
                } else
                {
                    throw new Exception($"Unknown verbose argument {arg}");
                }
            }
            else if(arg.StartsWith('-'))
            {
                if(arg.Length == 1)
                {
                    throw new Exception("'-' short argument form needs atleast one argument");
                }
                // shortform arguments
                foreach(char a in arg[1..])
                {
                    if(!_attributes.Keys.Contains(a))
                    {
                        throw new Exception($"Unknown short form argument {a} in {arg}, should it be --{args}?");
                    }
                    if(arguments.Contains(_attributes[a]))
                        throw new Exception($"Argument {a} in {arg} already use, --{_attributes[a]} or another -{a} ");
                    arguments.Add(_attributes[a]);
                }
                
    
            }
            else if(arg.Contains('?') || arg.Contains('*'))
            {
                    searchPatterns.Add(arg);
            }
            else
            {
                // Assumed filename
                files.Add(arg);
            }
        }

        // Changes to array for
        cm.Paths = [..files];
        cm.SearchPatterns = [..searchPatterns]; 
        cm.Arguments = [..arguments];
        return cm;
    }
    public override string ToString()
    {
        StringBuilder sb = new(100);
        foreach(string path in Paths)
        {
            sb.Append($"\nFile: {path}");
        }
        foreach(string serarchPattern in SearchPatterns)
            sb.Append($"\nSearchpattern: {SearchPatterns}");
        foreach(string argument in Arguments)
        {
            sb.Append($"\nArgument: {argument}");
        }
        return sb.ToString();
    }
}