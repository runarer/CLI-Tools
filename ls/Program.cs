
using System.Text;

string path;

// Check for arguments
if(args.Length == 0)
{
    path =  Directory.GetCurrentDirectory();
}
else if(Directory.Exists( args[0]))
{
    path = args[0];
} 
else
{
    Console.WriteLine($"File {args[0]} does not exist");
    return 1;    
}

// Get directories and files, seperate so we can print dirs first.
string[] directoriesInPath = Directory.GetDirectories(path);
string[] filesInDirectory = Directory.GetFiles(path);

foreach(string name in directoriesInPath)
    PrintFileInfoWide(name);
foreach(string name in filesInDirectory)
    PrintFileInfoWide(name);

return 0;

static string PrintFileInfoWide(string path)
{
    FileInfo info = new(path);
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