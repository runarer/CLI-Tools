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

string[] directoryContent = Directory.GetFiles(path,searchPattern);

foreach (string name in directoryContent)
{
    Console.WriteLine("- " + name);
}

return 0;
