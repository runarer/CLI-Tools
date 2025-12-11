
string output;

bool escapeSequence = false;
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
            switch(c)
            {
                case 'e': escapeSequence=true; 
                    break;
                case 'n': newLineAtEnd = false;
                    break;
                case 'h': HelpText();
                    return 0;
                default: InvalidArgument(c);
                         Usage(); 
                    return 1;
            }
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
}

static void Usage() {
    Console.WriteLine($"{System.Diagnostics.Process.GetCurrentProcess().ProcessName} -[enh] <your text");
}

static void HelpText()
{
    Usage();
    Console.WriteLine("Valid Arguments");
    Console.WriteLine("h\t-\tDisplay this text.");
    Console.WriteLine("e\t-\tEnable escape sequence");
    Console.WriteLine("n\t-\tSuspend newline");
}