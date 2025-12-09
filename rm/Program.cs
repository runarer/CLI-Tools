using System.Linq;
string path = args[0];
string searchPattern = String.Empty;

if(args.Length == 2)
    searchPattern = args[1];

var content = Directory.EnumerateFileSystemEntries(path,searchPattern,SearchOption.TopDirectoryOnly);
DeleteListed(content);

return 0;

bool ParseArguments(string[] args, Dictionary<string,char> attributes)
{


    return true;
}

static void DeleteListed(IEnumerable<string> list)
{
    // list.Reverse();
    foreach(string s in Enumerable.Reverse(list) )
    {
        Console.WriteLine(s);
    }
}

