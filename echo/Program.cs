
using System.Security.Cryptography.X509Certificates;

string output;

bool newLineAtEnd = true;

// Check for arguments
if (args.Length == 0) {
    output = "\n";
} else
{
    int start = 0;
    if(args[0].StartsWith('-'))
    {
        start = 1;
        foreach(char c in args[0][1..])
        {
            if(c == 'n') 
                newLineAtEnd = false;
            else if(c == 'h') 
                HelpText(); 
            else
                InvalidArgument(c); 
        }
    } 

    output = String.Join(' ',args[start..]);
}

// Print the output
if(newLineAtEnd)
    Console.WriteLine(output);
else
    Console.Write(output);
return 0;


static void InvalidArgument(char c)
{
    Console.WriteLine($"{c} is not a reconiced argument");
    Usage();
    System.Environment.Exit(1);
}

static void Usage() {
    Console.WriteLine($"{System.Diagnostics.Process.GetCurrentProcess().ProcessName} -[enh] <your text");
}

static void HelpText()
{
    Usage();
    Console.WriteLine("Valid Arguments");
    Console.WriteLine("h\t-\tDisplay this text.");
    Console.WriteLine("n\t-\tSuspend newline");
    System.Environment.Exit(0);
}