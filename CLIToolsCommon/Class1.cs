using System.Text;

namespace CLIToolsCommon;

public class CommandLineArguments
{
    public List<string> Paths {private set; get; } = [];
    public List<string> Arguments {private set; get; } = [];


    public CommandLineArguments(string[] args, Dictionary<char,string> _allowedArguments)
    {
        if(args.Length < 1)
            return;

        foreach(string arg in args)
        {
            if(arg.StartsWith("--"))
            {
                if(arg.Length == 2)
                    throw new Exception("-- lacks an argument");
                if(_allowedArguments.Values.Contains(arg[2..]))
                {
                    if(Arguments.Contains(arg[2..])) {
                        char value = _allowedArguments.First( v => v.Value == arg).Key;
                        throw new Exception($"Argument {arg} already used, another --{arg} or  -{value} ");
                    }
                   Arguments.Add(arg[2..]); 
                } else
                {
                    throw new Exception($"Unknown verbose argument {arg}");
                }
            }
            else if(arg.StartsWith('-'))
            {
                if(arg.Length == 1)
                {
                    throw new Exception("'-' short argument form needs atleast one argument");
                }
                // shortform arguments
                foreach(char a in arg[1..])
                {
                    if(!_allowedArguments.Keys.Contains(a))
                    {
                        throw new Exception($"Unknown short form argument {a} in {arg}");
                    }
                    if(Arguments.Contains(_allowedArguments[a]))
                        throw new Exception($"Argument {a} in {arg} already use, --{_allowedArguments[a]} or another -{a} ");
                    Arguments.Add(_allowedArguments[a]);
                }
                
    
            }
            else
            {
                // Assumed filename
                Paths.Add(arg);
            }
        }
    }
}