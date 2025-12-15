# CLI-Tools

Common code is in CLIToolsCommon.
It contain a class for parsing arguments; CommandLineArguments. 
The shell manipulates the commandline argument before it's sent to dotnet and
made available in args. This creates a lot of inconsistencies 


## echo
Use: echo [-nh] output

h display help
n removes newline at end for git terminal.


## cat
Use: cat [-nBsE|h] [--number|--number-nonblank|--squeeze-blank|--show-ends|--help] filenames

n for numbering lines
B for only number non-blank-lines
s for suppress blank lines when more than one
E for adding '$' at end of line


## ls
Use: ls <directory>
directory is optional.


## pwd
Use: pwd [-v|h|L|P] [--version|help|logical|physical]

v print version
h print help messsage
L for logical path (default)
P for physical path

## rm
Use: rm [-rvp|h] [--help|verbose|print|recursive] file|directory

r deletes content in subdirectories
v printing name of files and directories
p as verbose but do not delete file.
h for help message

## touch
Use: touch [-h|--help] filenames