using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Notes.ViewModel;
using Notes.View;
using Notes.Model;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }

        private void NoteList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AddNewNoteViewModel._note = e.Item as Note;
            //MessagingCenter.Send<Note>(e.Item as Note,"Note");
            Navigation.PushAsync(new AddNewNote());
        }
    }
}
