using Notes.ViewModel;
using Notes.Model;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrioritySetter : PopupPage
    {
        public PrioritySetter(Note note)
        {

            InitializeComponent();
            BindingContext = new PrioritySetterViewModel(Navigation,note);
        }
    }
}