using System.Linq;
string path = args[0];
string searchPattern = String.Empty;

if(args.Length == 2)
    searchPattern = args[1];

var content = GetDirectoryContent(path,searchPattern);
DeleteListed(content);

return 0;

static List<string> GetDirectoryContent(string path, string searchPattern)
{
    List<string> content = [];

    if(File.Exists(path))
    {
        content.Add(path);
    } 
    else if (Directory.Exists(path))
    {
        if(searchPattern == String.Empty) {
            content.Add(path);
        }
        string[] directoriesInPath = Directory.GetDirectories(path);
        string[] filesInDirectory = Directory.GetFiles(path,searchPattern);
        foreach(string directory in directoriesInPath) 
            content.AddRange(GetDirectoryContent(directory,searchPattern));
        content.AddRange(filesInDirectory);  
    } 

    return content;
}

static void DeleteListed(List<string> list)
{
    // list.Reverse();
    foreach(string s in Enumerable.Reverse(list) )
    {
        Console.WriteLine(s);
    }
}