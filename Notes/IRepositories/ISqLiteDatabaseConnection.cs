using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Notes.Model;
using System.Threading.Tasks;

namespace Notes.IRepositories
{
    public interface ISqLiteDatabaseConnection
    {
        SQLiteConnection GetConnectionWithDatabase();

        List<Note> GetNotes();

        void InsertNote(Note note);
        void UpdateNote(string query);
        void DeleteNote(int noteId);
    }
}
