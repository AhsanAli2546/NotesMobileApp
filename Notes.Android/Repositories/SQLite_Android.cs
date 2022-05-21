using Notes.IRepositories;
using Notes.Droid.Repositories;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SQLite;
using System.IO;
using Notes.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(SQLite_Android))]
namespace Notes.Droid.Repositories
{
    class SQLite_Android : ISqLiteDatabaseConnection
    {

        SQLiteConnection connection;
        public SQLiteConnection GetConnectionWithDatabase()
        {
            
            string dbname = "Notes.db";
            string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(dbpath, dbname);
           
            connection = new SQLiteConnection(path);
            connection.CreateTable<Note>();
            return connection;

        }
        

        public List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();

            try
            {
                notes = connection.Table<Note>().ToList();
            }
            catch (Exception ex)
            {   
            }
            


            return notes;

        }

        public void InsertNote(Note note)
        {
            List<Note> noteList = GetNotes();

            if (noteList.Count <= 0)
            {
                note.Priority = 1;
                note.ArrivalTime = 1;
                connection.Insert(note);
            }
            else
            {
                if(note.NoteId > 0)
                {

                    UpdateNote("Update Note Set Title = '" + note.Title + "', Detail = '" + note.Detail + "', NoteDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where NoteId = " + note.NoteId);
                }
                else
                {
                    
                    //if (noteList.Contains(note))
                    //{
                        //UpdateNote("Update Note Set Title = '" + note.Title + "',Detail = '" + note.Detail + " DateTime = '" + note.DateTime + "'Where NoteId = " + note.NoteId);
                    //}
                    //else
                    //{
                    Note lastNote = noteList.OrderByDescending(e => e.NoteId).FirstOrDefault();
                    note.ArrivalTime = lastNote.ArrivalTime + 1;
                    note.Priority = lastNote.Priority + 1;
                    connection.Insert(note);
                    //}
                    
                }
                
            }
        }

        public void UpdateNote(string query)
        {
            //string qry = "Update Note Set Title = '" + note.Title + "',Detail = '" + note.Detail + " DateTime = '" + note.DateTime + "'Where NoteId = " + note.NoteId;
            try
            {
                connection.Execute(query);
            }
            catch(Exception ex)
            {

            }
            
        }

        public void DeleteNote(int noteId)
        {
            try
            {
                connection.Delete<Note>(noteId);
            }
            catch(Exception ex)
            {
                
            }
            
          
        }
    }
}