using Notes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Notes.BusinessLogic
{
    class PriorityAlgorithm
    {
        public static List<Note> SortByPriority(List<Note> note)
        {
            //List<Note> noteList = new List<Note>();
            Note tempNote = new Note();
            

            note = note.OrderBy(e => e.ArrivalTime).ToList();

            for (int i = 0; i < note.Count - 1; i++)
            {
                if(note[i].ArrivalTime == note[i + 1].ArrivalTime)
                {
                    if(note[i + 1].Priority < note[i].Priority )
                    {
                        tempNote = note[i];
                        note[i] = note[i + 1];
                        note[i + 1] = tempNote;
                        
                        
                        //note.Insert(i, note[i + 1]);
                        
                        
                    }
                }
            }

            /*for (int i = 0; i < note.Count - 1; i++)
            {
                for (int j = 0; j < note.Count - 1; j++)
                {
                    if(note[j].ArrivalTime > note[j + 1].ArrivalTime)
                    {

                    }
                    
                }
            }*/

            return note;

        }
    }
}
