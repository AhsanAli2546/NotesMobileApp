using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Notes.View;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Notes.Model;
using Notes.IRepositories;
using System.Collections.ObjectModel;
using System.Linq;
using Notes.BusinessLogic;


namespace Notes.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public INavigation iNavigation { get; set; }

        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Note> _Collection;

        public ObservableCollection<Note> Collection
        {
            get { return _Collection; }
            set { _Collection = value; OnPropertyChanged(); }
        }

        public MainPageViewModel(INavigation navigation)
        {
            _Collection = new ObservableCollection<Note>();
            DependencyService.Get<ISqLiteDatabaseConnection>().GetConnectionWithDatabase();
            LoadList();

            
            iNavigation = navigation;
            
            
        }

        public void LoadList()
        {
            List<Note> note = DependencyService.Get<ISqLiteDatabaseConnection>().GetNotes();
            note = PriorityAlgorithm.SortByPriority(note);
            ObservableCollection<Note> obj = new ObservableCollection<Note>();

            if(note.Count > 0)
            {

                foreach (var item in note)
                {
                    obj.Add(item);
                }

                //_Collection.Clear();
                Collection = obj;
            }

        }


        public Command RefreshCommand
        {

            get
            {

                return new Command(() =>
                {
                    IsBusy = true;
                    LoadList();
                    IsBusy = false;
                });
            }
        }

        public Command AddNoteCommand
        {

            get
            {

                return new Command(() =>
                {
                    AddNewNoteViewModel._note = null;
                    iNavigation.PushAsync(new AddNewNote());
                    //LoadList();
                    //MessagingCenter.Subscribe<Note>(this, "NewNote", (value) => {

                    //Collection.Add(new Note { Title = value.Title, DateTime = value.DateTime, Detail = value.Detail });

                    //});
                });
            }
        }

        public Command DeleteNoteCommand
        {

            get
            {

                return new Command((object obj)=> {
                    Note note = new Note();

                    note = (Note)obj;

                    DependencyService.Get<ISqLiteDatabaseConnection>().DeleteNote(note.NoteId);
                    LoadList();
                });
            }
        }

        public Command SetPriorityCommand
        {

            get
            {

                return new Command((object obj) =>
                {
                    //Note note = new Note();
                    //note = (Note)obj;
                    iNavigation.PushModalAsync(new PrioritySetter((Note)obj));
                    
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        
    }
}
