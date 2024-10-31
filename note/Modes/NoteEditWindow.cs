using Terminal.Gui;

namespace note.Modes;

public sealed class NoteEditWindow : Window {
    private TextView NoteField { get; set; }

    public NoteEditWindow(Note? editNote = null) {
        var note = editNote ?? Modes.NotesController.CreateNote();
        Title = note.Path;
        ColorScheme = Colors.TopLevel;

        var hintHotKeySave = new Label("Ctrl+S: Save Note | Ctrl+Q: Quit (auto save if note is not empty)");
        NoteField = new TextView {
            Text = note.Text,
            Y = Pos.Bottom(hintHotKeySave),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Colors.Dialog,
            WordWrap = true
        };
        KeyDown += args => {
            var e = args.KeyEvent;
            if (e.ToString() == "Ctrl-S" || e.ToString() == "Ctrl-Q") {
                note.Text = NoteField.Text.ToString() ?? "";
                Modes.NotesController.SaveNote(note);
            }
        };
        Add([hintHotKeySave, NoteField]);
        NoteField.FocusFirst();
    }
}