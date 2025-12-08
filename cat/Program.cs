if (args.Length < 1)
{
    Console.WriteLine("Usage: cat filename");
    return 0;
}

string filename = args[0];

string fileContent;
try
{
    fileContent = File.ReadAllText(filename);
} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return 1;
}

Console.WriteLine(fileContent);


return 0;