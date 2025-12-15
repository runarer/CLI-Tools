
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
    Console.WriteLine($"Directory {args[0]} does not exist");
    return 1;    
}

// Get directories and files, seperate so we can print dirs first.
IEnumerable<string> directories = Directory.EnumerateDirectories(path);
IEnumerable<string> files = Directory.EnumerateFiles(path);
IEnumerable<string> content = directories.Concat(files);

// Print the content
foreach(string name in content)
    Console.WriteLine(PrintFileInfoWide(name));

return 0;

static string PrintFileInfoWide(string path)
{
    FileInfo info = new(path);
    StringBuilder sb =  new ();
    bool file = true;
    
    // Check if directory
    if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory )
        file = false;
    
    if(file)
        sb.Append("File  ");
    else
        sb.Append("Dir   ");

    // Date and time for last modification
    sb.Append($"{info.LastAccessTime.Date.ToShortDateString()}  {info.LastAccessTime.ToShortTimeString()}");
    
    // Size of file, ' ' as separator every 3 digits
    if(file)
        sb.Append($"{info.Length.ToString("N0",new System.Globalization.CultureInfo("fr-FR")),16}");
    else
        sb.Append(new string(' ',16));
    
    sb.Append($"  {info.Name}");

    return sb.ToString();
}