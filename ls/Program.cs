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

printContent(directoriesInPath);
printContent(filesInDirectory);

return 0;


static void printContent(string[] content)
{
    foreach(string c in content)
        Console.WriteLine(c);
}