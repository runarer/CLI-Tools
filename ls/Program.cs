
using System.Drawing;
using System.Text;

string path;
string searchPattern = "";

if(args.Length == 0)
{
    path =  Directory.GetCurrentDirectory();
}
else
{
    path = args[0];
}

if(!Directory.Exists(path))
{
    
    // Could be an argument like -l
    //If file doesn't exist it could be a searchPattern like *.txt
    searchPattern = args[0];
    path = Directory.GetCurrentDirectory();
}

string[] directoriesInPath = Directory.GetDirectories(path);
string[] filesInDirectory = Directory.GetFiles(path,searchPattern);

// printContent(directoriesInPath);
// printContent(filesInDirectory);
PrintDirectoryContentWide(directoriesInPath);
PrintDirectoryContentWide(filesInDirectory);


return 0;


static void PrintDirectoryContentWide(string[] content)
{
    foreach(string path in content)
    {
        Console.WriteLine(PrintFileInfo(new FileInfo(path)));
    }
}

static string PrintFileInfo(FileInfo info)
{
    StringBuilder sb =  new ();
    bool file = true;
    
    if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory )
    file = false;
    
    if(file)
        sb.Append("File  ");
    else
        sb.Append("Dir   ");

    sb.Append($"{info.LastAccessTime.Date.ToShortDateString()}  {info.LastAccessTime.ToShortTimeString()}");
    
    if(file)
        sb.Append($"{info.Length.ToString("N0",new System.Globalization.CultureInfo("fr-FR")),16}");
    else
        sb.Append(new string(' ',16));
    
    sb.Append($"  {info.Name}");

    return sb.ToString();
}