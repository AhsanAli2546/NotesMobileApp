using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Notes.Model;
using Notes.IRepositories;

namespace Notes.ViewModel
{
    class AddNewNoteViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _lblDateTime;

        public string lblDateTime
        {
            get { return _lblDateTime; }
            set { _lblDateTime = value; OnPropertyChanged(); }
        }

        private string _tbTitle;

        public string tbTitle
        {
            get { return _tbTitle; }
            set { _tbTitle = value; OnPropertyChanged(); }
        }

        private string _editDetail;

        public string editDetail
        {
            get { return _editDetail; }
            set { _editDetail = value; OnPropertyChanged(); }
        }

        private ImageSource _SaveImageButton;

        public ImageSource SaveImageButton
        {
            get { return _SaveImageButton; }
            set { _SaveImageButton = value; OnPropertyChanged(); }
        }
        //Code to change the resource
        //ImageSource myImageSource = ImageSource.FromResource("NameOfProject.Resources.image.png");



        #endregion


        public static Note _note = new Note();

        public AddNewNoteViewModel()
        {
            //Source="{local:ImageResource Notes.Images.tick.png}"
            _SaveImageButton = ImageSource.FromResource("Notes.Images.tick.png");
            try
            {
                tbTitle = AddNewNoteViewModel._note.Title;
                lblDateTime = AddNewNoteViewModel._note.NoteDateTime.ToString("dddd, dd MMMM yyyy");
                editDetail = AddNewNoteViewModel._note.Detail;
                
            }
            catch (Exception ex)
            {

                lblDateTime = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            }
            //if(AddNewNoteViewModel._note.Title == null || AddNewNoteViewModel._note.Title == String.Empty)
            //{
            //    lblDateTime = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            //}
            //else
            //{
            //    tbTitle = AddNewNoteViewModel._note.Title;
            //    lblDateTime = AddNewNoteViewModel._note.DateTime.ToString("dddd, dd MMMM yyyy");
            //    editDetail = AddNewNoteViewModel._note.Detail;
            //    AddNewNoteViewModel._note = null;
            //}


            MessagingCenter.Subscribe<Note>(this, "Note", (value) => {

                AddNewNoteViewModel._note = value;

            });
        }

        

        public Command SaveNoteCommand { 
            
            get { 
                return new Command(() => {

                    
                    try
                    {
                        Note newNote = new Note();

                        newNote.NoteId = 0;
                        newNote.Title = tbTitle;
                        newNote.Detail = editDetail;
                        newNote.NoteDateTime = Convert.ToDateTime(lblDateTime);

                        if (_note != null)
                        {
                            newNote.NoteId = _note.NoteId;
                        }
                        
                        DependencyService.Get<ISqLiteDatabaseConnection>().InsertNote(newNote);
                        
                        
                    }
                    catch (Exception ex)
                    {
                        //Note newNote = new Note();
                        //newNote.Title = tbTitle;
                        //newNote.DateTime = Convert.ToDateTime(lblDateTime);
                        //newNote.Detail = editDetail;
                        //DependencyService.Get<ISqLiteDatabaseConnection>().InsertNote(newNote);

                    }
                    
                    

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
