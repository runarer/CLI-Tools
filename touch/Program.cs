using CLIToolsCommon;

if(args.Length == 0)
{
    Usage();
    return 0;
}

CommandLineArguments cm;
try
{
    Dictionary<char,string> validArguments = new(){ {'L',"logical"},{'P',"physical"},{'h',"help"},{'v',"version"}};
    cm = new(args,validArguments);
} catch(Exception ex)
{
    Console.WriteLine(ex);
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


static void Usage()
{
        Console.WriteLine("Usage: touch filename");

}