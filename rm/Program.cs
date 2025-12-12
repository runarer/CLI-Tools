
using CLIToolsCommon;

Dictionary<char,string> d1 = new(){ {'h',"help"},{'r',"recursive"},{'v',"verbose"},{'p',"print"}};
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
    Usage();
    return 0;
}

if(cm.Arguments.Contains("help"))
{
    Help();
    return 0;
}

SearchOption searchOption = SearchOption.TopDirectoryOnly;
if (cm.Arguments.Contains("recursive"))
    searchOption = SearchOption.AllDirectories;

List<string> content = [];

foreach(string path in cm.Paths)
{
    if (File.Exists(path))
    {
        content.Add(path);
    } 
    else if (Directory.Exists(path)) 
    {
        content.AddRange(Directory.EnumerateFileSystemEntries(path,"",searchOption));
    }
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

static void Usage()
{
    Console.WriteLine($"Usage: rm <arguments> <searchPattern> <filenames ..>");
}

static void Help()
{
    Usage();
    Console.WriteLine("-h --help\t\tPrint this message");
    Console.WriteLine("-v --verbose\t\tPrint names to output");
    Console.WriteLine("-p --print\t\tDon't delete files but print the names");
    Console.WriteLine("-r --recursive\t\tDelete files in subdirectories");
}

