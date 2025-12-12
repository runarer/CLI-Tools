# CLI-Tools

Common code is in CLIToolsCommon.
It contain a class for parsing arguments; CommandLineArguments. 
The shell manipulates the commandline argument before it's sent to dotnet and
made available in args. This creates a lot of inconsistencies 

My attemt at commandline parsing should be replaced with
(System.CommandLine)[https://www.nuget.org/packages/System.CommandLine].

## echo
Use: echo [-nh] output

h display help
n removes newline at end for git terminal.

## cat
Use: cat [-nBsE|h] [--number|--number-nonblank|--squeeze-blank|--show-ends|--help] filenames

## ls

## pwd
Use: pwd [-v|h|L|P] [--version|help|logical|physical]

default is logical, physical is not tested yet.

## rm
Use: rm [-rvp|h] [--help|verbose|print|recursive] file|directory

## touch
Use: touch [-h|--help] filenames