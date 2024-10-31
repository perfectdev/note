using Terminal.Gui;

namespace note;

public record Note(string Path, string Text) {
    public string Path { get; set; } = Path;
    public string Text { get; set; } = Text;
    public override string ToString() => Text[..(Text.Length > 40 ? 40 : Text.Length)];
}