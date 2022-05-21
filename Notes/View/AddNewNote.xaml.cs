using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewNote : ContentPage
    {
        public AddNewNote()
        {
            InitializeComponent();
            BindingContext = new AddNewNoteViewModel();
            
        }

        
    }
}