using Terminal.Gui;

namespace note.Modes;

public sealed class ListNotesWindow : Window {
    private ListView ListNotes { get; set; }
    private TextView? NoteField { get; set; }
    private Label NoteEditTitle { get; set; }
    private Note? SelectedNote { get; set; }
    private List<Note> SelectedNotes { get; set; } = [];

    public ListNotesWindow() {
        Modes.NotesController.LoadNotes();
        this.ColorScheme = Colors.TopLevel;
        Title = $"All notes by {Environment.UserName}";
        var hintHotKeySave = new Label("Ctrl+S: Save Note | Ctrl+Q: Quit (auto save if note is not empty)");
        var frame = new FrameView {
            Y = Pos.Bottom(hintHotKeySave),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };
        ListNotes = new ListView {
            CanFocus = true,
            AllowsMarking = false,
            AllowsMultipleSelection = false,
            Width = Dim.Sized(40),
            Height = Dim.Fill()
        };
        var textFrame = new FrameView {
            X = Pos.Right(ListNotes),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };
        NoteEditTitle = new Label($"{SelectedNote?.Path}") {
            Width = Dim.Fill(),
            Height = Dim.Sized(1)
        };
        NoteField = new TextView {
            Text = SelectedNote?.Text ?? "",
            Enabled = false,
            Y = Pos.Bottom(NoteEditTitle),
            ColorScheme = Colors.Base,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        textFrame.Add([NoteEditTitle, NoteField]);
        ListNotes.SetSourceAsync(Modes.NotesController.Notes);
        frame.Add([ListNotes, textFrame]);
        Add([hintHotKeySave, frame]);
        KeyDown += OnKeyDown;
        ListNotes.SelectedItemChanged += ListNotesOnSelectedItemChanged;
        NoteField.Leave += NoteFieldOnLeave;
    }

    private void NoteFieldOnLeave(FocusEventArgs args) {
        if (SelectedNote is null)
            return;
        SelectedNote.Text = NoteField?.Text?.ToString() ?? string.Empty;
        Modes.NotesController.SaveNote(SelectedNote);
    }

    private void OnKeyDown(KeyEventEventArgs args) {
        var e = args.KeyEvent;
        if (e.ToString() == "Ctrl-S" || e.ToString() == "Ctrl-Q") {
            if (SelectedNote is null) 
                return;
            SelectedNote.Text = NoteField!.Text?.ToString() ?? "";
            Modes.NotesController.SaveNote(SelectedNote);
        }
    }

    private void ListNotesOnSelectedItemChanged(ListViewItemEventArgs args) {
        NoteField!.Enabled = false;
        if (args.Value is Note note) {
            SelectedNote = note;
            NoteEditTitle.Text = SelectedNote.Path;
            NoteField!.Text = SelectedNote.Text;
            NoteField.Enabled = true;
        }
    }
}