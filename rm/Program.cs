using System.Linq;
using System.Text;
string path = args[0];
string searchPattern = String.Empty;

if(args.Length == 2)
    searchPattern = args[1];

var content = Directory.EnumerateFileSystemEntries(path,searchPattern,SearchOption.TopDirectoryOnly);
DeleteListed(content);

return 0;

static void DeleteListed(IEnumerable<string> list)
{
    // list.Reverse();
    foreach(string s in Enumerable.Reverse(list) )
    {
        Console.WriteLine(s);
    }
}

class CommandLineArguments(Dictionary<string,char> attributes)
{
    public List<string> Paths {private set; get; } = [];
    public string SearchPattern {private set; get; } = string.Empty;
    public string Arguments {private set; get; } = string.Empty;

    private Dictionary<string,char> _attributes = attributes;

    public bool ParseArguments(string[] args)
    {
        StringBuilder shortForm = new(10);
        foreach(string arg in args)
        {
            if(arg.StartsWith("--"))
            {
                if(arg.Length == 2)
                    throw new Exception("-- lacks an argument");
                if(_attributes.Keys.Contains(arg[2..]))
                {
                   shortForm.Append(_attributes[arg[2..]]); 
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
                    if(!_attributes.Values.Contains(a))
                    {
                        throw new Exception($"Unknown short form argument {a}");
                    }
                }
                shortForm.Append(arg[1..]);
    
            }
            else if(arg.Contains('?') || arg.Contains('*'))
            {
                // search pattern
                if(SearchPattern != string.Empty)
                {
                    SearchPattern = arg;
                }
                else
                {
                    throw new Exception($"More than one search pattern found: {SearchPattern} and {arg}");
                }
            }
            else
            {
                // Assumed filename
                Paths.Add(arg);
            }
        }

        Arguments = shortForm.ToString();
        return true;
    }
}