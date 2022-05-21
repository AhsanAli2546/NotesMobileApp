using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Notes.Model
{
    public class Note
    {
        [PrimaryKey,AutoIncrement]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime NoteDateTime { get; set; }
        public int ArrivalTime { get; set; }
        public int Priority { get; set; }
    }
}
