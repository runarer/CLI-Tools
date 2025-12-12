
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

bool recursive = false;
if (cm.Arguments.Contains("recursive"))
    recursive = true;

List<string> content = [];
List<string> files = [];
List<string> directories = [];

foreach(string path in cm.Paths)
{
    if (File.Exists(path))
    {
        files.Add(path);
    } 
    else if (Directory.Exists(path)) 
    {
        if(recursive) {       
            foreach(string subPath in Directory.EnumerateFileSystemEntries(path,"",SearchOption.AllDirectories) )
            {
                if(File.Exists(subPath))
                    files.Add(subPath);    
                 else if(Directory.Exists(subPath))
                    directories.Add(subPath);
            }
        }
        else
            directories.Add(path);
    }
}

foreach(string path in files)
    DeletePath(path,File.Delete,cm.Arguments.Contains("print"),cm.Arguments.Contains("verbose"));

foreach(string path in directories)
    DeletePath(path,Directory.Delete,cm.Arguments.Contains("print"),cm.Arguments.Contains("verbose"));

return 0;

// static void DeleteListed(IEnumerable<string> list,bool onlyPrint, bool verbose)
// {
//     // list.Reverse();
//     foreach(string s in Enumerable.Reverse(list) )
//     {
//         string path = Path.GetFullPath(s);
//         if(onlyPrint || verbose)
//             Console.WriteLine(s);
//         if(!onlyPrint)
//             Console.WriteLine("Replace me with delete command" + s);
//     }
// }

static void DeletePath(string path, Action<string> action, bool onlyPrint,bool verbose)
{
    string fullPath = Path.GetFullPath(path);
    if(onlyPrint || verbose)
        Console.WriteLine(fullPath);
    if(!onlyPrint) {
        // Console.WriteLine("Replace me with delete command" + fullPath);
        try
        {
            action(fullPath);
        } 
        catch (IOException)
        {
            Console.WriteLine($"{path} does not exists or can be deleted");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"{path} you don't have access");
        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(3);
        }
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

