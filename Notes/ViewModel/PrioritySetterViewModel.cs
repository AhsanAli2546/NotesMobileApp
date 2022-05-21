using Notes.IRepositories;
using Notes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Notes.ViewModel
{
    public class PrioritySetterViewModel : INotifyPropertyChanged
    {
        #region UIProperties
        private string _tbArrivalTime;

        public string tbArrivalTime
        {
            get { return _tbArrivalTime; }
            set { _tbArrivalTime = value; OnPropertyChanged(); }
        }

        private string _tbPriority;

        public string tbPriority
        {
            get { return _tbPriority; }
            set { _tbPriority = value; }
        }
        #endregion

        private Note _Note = new Note();

        public INavigation iNavigation { get; set; }
        public PrioritySetterViewModel(INavigation inavigation,Note note)
        {
            iNavigation = inavigation;
            _tbArrivalTime = note.ArrivalTime.ToString();
            _tbPriority = note.Priority.ToString();
            _Note = note;
        }

        public Command CloseCommand
        {

            get
            {

                return new Command(() =>
                {

                    iNavigation.PopModalAsync();

                });
            }
        }

        public Command SavePriorityCommand
        {
            get
            {
                return new Command(() =>
                {
                    DependencyService.Get<ISqLiteDatabaseConnection>()
                    .UpdateNote("Update Note Set ArrivalTime = " + int.Parse(tbArrivalTime) + ", Priority = " + int.Parse( tbPriority) + " Where NoteId = " + _Note.NoteId);
                    iNavigation.PopModalAsync();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
