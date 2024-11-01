namespace note;

public class NotesController {
    private string NotesPath { get; }
    public List<Note> Notes { get; } = [];

    public NotesController() {
        NotesPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".notes");
        if (Directory.Exists(NotesPath)) 
            return;
        Directory.CreateDirectory(NotesPath);
    }
    
    public void LoadNotes() {
        Notes.Clear();
        var files = Directory.GetFiles(NotesPath, "*.note");
        foreach (var file in files) {
            var content = File.ReadAllText(file);
            Notes.Add(new Note(file, content));
        }
    }

    public Note CreateNote() {
        var idx = 0;
        var filePath = Path.Join(NotesPath, $"{DateTime.Now:yyyyMMddHHmmss}-{idx}.note");
        while (File.Exists(filePath)) {
            idx++;
            filePath = Path.Join(NotesPath, $"{DateTime.Now:yyyyMMddHHmmss}-{idx}.note");
        }
        var newNote = new Note(filePath, "");
        SaveNote(newNote);
        return newNote;
    }

    public void SaveNote(Note note) {
        try {
            if (string.IsNullOrWhiteSpace(note.Text.Trim())) {
                DeleteNote(note);
                return;
            }
            File.WriteAllText(note.Path, note.Text);    
        }
        catch (Exception e) {
            Console.WriteLine(e);          
        }
    }

    public void DeleteNote(Note note) {
        if (File.Exists(note.Path))
            File.Delete(note.Path);
        Notes.Remove(note);
    }
}