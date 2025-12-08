if(args.Length == 0)
{
    Console.WriteLine("Usage: touch filename");
    return 0;
}

string filename = args[0];

if(File.Exists(filename))
{
    Console.WriteLine("File allready Exisit");
    return 1;
}

try
{
    File.Create(args[0]);
} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return 1;
}

return 0;
