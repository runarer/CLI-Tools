
// using System.Linq;
using System.Text;

Dictionary<char,string> d1 = new(){ {'r',"recursive"},{'v',"verbose"},{'p',"print"}};
CommandLineArguments cm;
try {
    cm = new(args,d1);
} catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return 2;
}

if(cm.Paths.Count < 1 )
{
    PrintUsage();
    return 0;
}
SearchOption searchOption = SearchOption.TopDirectoryOnly;
if (cm.Arguments.Contains("recursive"))
    searchOption = SearchOption.AllDirectories;

IEnumerable<string> content;
if(cm.SearchPatterns.Count < 1 )
{
    content = Directory.EnumerateFileSystemEntries(cm.Paths[0],"",searchOption);
} else
{
    content = Directory.EnumerateFileSystemEntries(cm.Paths[0],cm.SearchPatterns[0],searchOption);    
}

DeleteListed(content,cm.Arguments.Contains("print"),cm.Arguments.Contains("verbose"));

return 0;

static void DeleteListed(IEnumerable<string> list,bool onlyPrint, bool verbose)
{
    // list.Reverse();
    foreach(string s in Enumerable.Reverse(list) )
    {
        if(onlyPrint || verbose)
            Console.WriteLine(s);
        if(!onlyPrint)
            Console.WriteLine("Replace me with delete command" + s);
    }
}

static void PrintUsage()
{
    Console.WriteLine($"Usage: {System.Diagnostics.Process.GetCurrentProcess().ProcessName} <arguments> <searchPattern> <filenames ..>");
}

class CommandLineArguments
{
    public List<string> Paths {private set; get; } = [];
    public List<string> SearchPatterns {private set; get; } = [];
    public List<string> Arguments {private set; get; } = [];


    public CommandLineArguments(string[] args, Dictionary<char,string> _allowedArguments)
    {
        // HashSet<string> files = [];
        // HashSet<string> searchPatterns = [];
        // HashSet<string> arguments = [];

        if(args.Length < 1)
            return;

        foreach(string arg in args)
        {
            if(arg.StartsWith("--"))
            {
                if(arg.Length == 2)
                    throw new Exception("-- lacks an argument");
                if(_allowedArguments.Values.Contains(arg[2..]))
                {
                    if(Arguments.Contains(arg[2..])) {
                        char value = _allowedArguments.First( v => v.Value == arg).Key;
                        throw new Exception($"Argument {arg} already used, another --{arg} or  -{value} ");
                    }
                   Arguments.Add(arg[2..]); 
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
                    if(!_allowedArguments.Keys.Contains(a))
                    {
                        throw new Exception($"Unknown short form argument {a} in {arg}, should it be --{args}?");
                    }
                    if(Arguments.Contains(_allowedArguments[a]))
                        throw new Exception($"Argument {a} in {arg} already use, --{_allowedArguments[a]} or another -{a} ");
                    Arguments.Add(_allowedArguments[a]);
                }
                
    
            }
            else if(arg.Contains('?') || arg.Contains('*'))
            {
                    SearchPatterns.Add(arg);
            }
            else
            {
                // Assumed filename
                Paths.Add(arg);
            }
        }
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