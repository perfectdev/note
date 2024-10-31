using System.Reflection;
using note.Modes;
using static Crayon.Output;

if (args.Contains("--help") || args.Contains("-h")) {
    Console.WriteLine("Notes for usage:");
    Console.WriteLine($"\t{Bold().Green("list")}\t{Dim("Show list of notes")}");
    Environment.Exit(0);
}

if (args.Contains("--version") || args.Contains("-v")) {
    var assembly = Assembly.GetExecutingAssembly();
    Console.WriteLine($"\t{Bold().Green(assembly.GetName().Name ?? string.Empty)} {Dim(assembly.GetName().Version?.ToString() ?? string.Empty)}");
    Environment.Exit(0);
}

if (args.Contains("list")) {
    Modes.ListNotes();
    Environment.Exit(0);
}

Modes.CreateNote();