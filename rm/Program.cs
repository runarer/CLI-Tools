
// using System.Linq;
using System.Text;

using CLIToolsCommon;

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
