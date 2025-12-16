
using CLIToolsCommon;

// Parse commandline input
Dictionary<char,string> d1 = new(){ {'h',"help"},{'r',"recursive"},{'v',"verbose"},{'p',"print"}};
CommandLineArguments cm;

try {
    cm = new(args,d1);
} catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return 2;
}

// If no input display usage and exit
if(cm.Paths.Count < 1 )
{
    Usage();
    return 0;
}

// User ask for help, display and exit
if(cm.Arguments.Contains("help"))
{
    Help();
    return 0;
}

// From commandling, seperate files and directories
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
        directories.Add(path);
    }
}

// Delete files and direcories
foreach(string path in files)
    DeleteFile(path,cm.Arguments.Contains("print"),cm.Arguments.Contains("verbose"));

foreach(string path in directories)
    DeleteDirectory(path,cm.Arguments.Contains("recursive"),cm.Arguments.Contains("print"),cm.Arguments.Contains("verbose"));

return 0;

// Delete file
// path is file to delete
// onlyPrint if no delete
// verbose for writing files that are to be delete to output
static void DeleteFile(string path, bool onlyPrint,bool verbose)
{
    string fullPath = Path.GetFullPath(path);
    if(onlyPrint || verbose)
        Console.WriteLine(fullPath);
    if(!onlyPrint) {
        try
        {
            File.Delete(fullPath);
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

// Delete directory
// path is directory to delete
// recursive removes content in subdirectories
// onlyPrint if no delete
// verbose for writing files/dierctories that are to be delete to output
static void DeleteDirectory(string path, bool recursive, bool onlyPrint,bool verbose)
{
    string fullPath = Path.GetFullPath(path);
    // Print directory and content if needed
    if(onlyPrint || verbose) {        
        Console.WriteLine(fullPath);
        if(recursive)
        {
            foreach(string subpath in Directory.EnumerateFileSystemEntries(fullPath,"",SearchOption.AllDirectories))
                Console.WriteLine(subpath);
        }
    }
    // Delete directory and files
    if(!onlyPrint) {
        try
        {
            Directory.Delete(fullPath,recursive);
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
    Console.WriteLine($"Usage: rm <arguments> <filenames ..>");
}

static void Help()
{
    Usage();
    Console.WriteLine("-h --help\t\tPrint this message");
    Console.WriteLine("-v --verbose\t\tPrint names to output");
    Console.WriteLine("-p --print\t\tDon't delete files but print the names");
    Console.WriteLine("-r --recursive\t\tDelete files in subdirectories");
}

