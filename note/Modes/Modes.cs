using Terminal.Gui;

namespace note.Modes;

public static class Modes {
    public static NotesController NotesController;

    static Modes() {
        NotesController = new NotesController();
    } 
    
    public static void CreateNote() {
        Application.Init();
        using var top = new Toplevel();
        top.Add(new NoteEditWindow());
        Application.Run(top);
        Application.Shutdown();
    }
    
    public static void ListNotes() {
        Application.Init();
        using var top = new Toplevel();
        top.Add(new ListNotesWindow());
        Application.Run(top);
        Application.Shutdown();
    }
}