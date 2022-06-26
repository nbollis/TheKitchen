using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace Fork
{
    public class RecipeDisplayViewModel : BaseViewModel, IFileDragDropTarget
    {

    #region Private Members

        private BitmapImage picture;
        private List<string> acceptedPictureFormats = new List<string> { ".jpg", ".png", ".jpeg"};
        private string name;
        private int _serves;
        private ObservableCollection<string> _procedure;
        private ObservableCollection<string> _notes;
        private ObservableCollection<string> _tags;
        private ObservableCollection<Ingredient> _ingredients;
        private ObservableCollection<CookInstance> _cookInstances;
        private string _description;
        private double _averageRating;
        private string _cookTime;
        private string _infoString;
        private int _timesCooked;

    #endregion

    #region Public Properties

        // represent the recipe data object
        public Recipe Recipe { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (!Recipe.Name.Equals(value))
                {
                    NameHasChanged = true;
                    HasChanged = true;
                }
                name = value;
                Recipe.Name = value;
                OnPropertyChanged(nameof(Name));   
            }
        }
        public int Serves
        {
            get { return _serves; }
            set { _serves = value; Recipe.Serves = value; 
                OnPropertyChanged(nameof(Serves)); }
        }
        public ObservableCollection<string> Procedure
        {
            get { return _procedure; }
            set { _procedure = value; Recipe.Procedure = value.ToList(); 
                OnPropertyChanged(nameof(Procedure)); }
        }
        public ObservableCollection<string> Notes
        {
            get { return _notes; }
            set { _notes = value; Recipe.Notes = value.ToList(); 
                OnPropertyChanged(nameof(Notes)); }
        }
        public ObservableCollection<string> Tags
        {
            get { return _tags; }   
            set {_tags = value; Recipe.Tags = value.ToList(); 
                OnPropertyChanged(nameof(Tags)); }
        }
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; Recipe.Ingredients = value.ToList(); 
                OnPropertyChanged(nameof(Ingredients)); }
        }
        public ObservableCollection<CookInstance> CookInstances
        {
            get { return _cookInstances; }
            set { _cookInstances = value; Recipe.CookInstances = value.ToList(); 
                OnPropertyChanged(nameof(CookInstances)); }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; Recipe.Description = value; 
                OnPropertyChanged(nameof(Description)); }
        }
        public double AverageRating
        {
            get { return _averageRating; }
            set { _averageRating = value;
                OnPropertyChanged(nameof(AverageRating)); }
        }
        public string CookTime
        {
            get { return _cookTime; }
            set { _cookTime = value;  
                OnPropertyChanged(nameof(CookTime)); }
        }
        public string InfoString
        {
            get { return _infoString; }
            set { _infoString = value;
                OnPropertyChanged(nameof(InfoString)); }
        }

        public int TimesCooked
        {
            get { return _timesCooked; }
            set { _timesCooked = value; 
                OnPropertyChanged(nameof(TimesCooked)); }
        }

        // ViewModel specific
        public bool HasChanged { get; set; } = false;
        public bool NameHasChanged { get; set; } = false;
        public int BufferThickness { get; set; } = 20;
        public BitmapImage Picture 
        { 
            get { return picture; }  
            set
            {
                picture = value;
                OnPropertyChanged(nameof(Picture));
            }
        }
        public string ImagePath { get; set; }

    #endregion

    #region Commands

        public ICommand AddToMealPrepCommand { get; set; }
        public ICommand DownloadRecipeCommand { get; set; }
        public ICommand PrintRecipeCommand { get; set; }
        public ICommand EditRecipeCommand { get; set; }
        public ICommand CommentRecipeComamnd { get; set; }
        public ICommand AddPictureCommand { get; set; }
        public ICommand SaveEditedRecipeCommand { get; set; }

    #endregion

    #region Constructor

        public RecipeDisplayViewModel(Recipe recipe)
        {
            Recipe = recipe;
            Name = recipe.Name;
            Serves = recipe.Serves;
            Procedure = new ObservableCollection<string>(recipe.Procedure);
            Notes = new ObservableCollection<string>(recipe.Notes);
            Tags = new ObservableCollection<string>(recipe.Tags);
            Ingredients = new ObservableCollection<Ingredient>(recipe.Ingredients);
            CookInstances = new ObservableCollection<CookInstance>(recipe.CookInstances);
            Description = recipe.Description;

            if (recipe.ImageFilePath != null)
            {
                picture = new BitmapImage(new Uri(recipe.ImageFilePath));
            }
            CalculateAverageValues();

            AddToMealPrepCommand = new RelayCommand(() => AddToMealPrep());
            DownloadRecipeCommand = new RelayCommand(() => DownloadRecipe());
            PrintRecipeCommand = new RelayCommand(() => PrintRecipe());
            EditRecipeCommand = new RelayCommand(() => EditRecipe());
            CommentRecipeComamnd = new RelayCommand(() => CommentRecipe());
            AddPictureCommand = new RelayCommand(() => AddPicture());
            SaveEditedRecipeCommand = new RelayCommand(() => SaveEditedRecipe());
        }

    #endregion

        public void OnFileDrop(string[] filepaths)
        {
            if (filepaths.Count() > 1)
            {
                MessageBox.Show("Only One Picture Allowed");
                return;
            }
            AddPicture(filepaths[0]);
            HasChanged = true;
        }

        public void CalculateAverageValues()
        {
            TimesCooked = CookInstances.Count();
            if (CookInstances.Count > 0)
            {
                AverageRating = CookInstances.Select(p => p.Rating).Average();
                double avgTime = CookInstances.Select(p => p.ProductionTime.Minutes).Average();
                CookTime = Converters.GetTimeString(TimeSpan.FromMinutes(avgTime));
            }
        }

    #region Private Helpers

        

    #endregion

    #region Command Methods

        private void AddToMealPrep()
        {

        }

        private void DownloadRecipe()
        {

        }

        private void PrintRecipe()
        {

        }

        private void EditRecipe()
        {
            EditRecipeWindowControl editRecipeWindowControl = new(this);
            WindowViewModel windowViewModel = new(editRecipeWindowControl);
            editRecipeWindowControl.DataContext = windowViewModel;
            editRecipeWindowControl.ShowDialog();
        }

        private void CommentRecipe()
        {
            AddCookInstanceWindowControl addCookInstanceWindowControl = new(this);
            WindowViewModel windowViewModel = new(addCookInstanceWindowControl);
            addCookInstanceWindowControl.DataContext = windowViewModel;
            addCookInstanceWindowControl.ShowDialog();

            if (addCookInstanceWindowControl.DialogResult == true)
            {
                HasChanged = true;
                string notes = new TextRange(addCookInstanceWindowControl.NotesBox.Document.ContentStart, addCookInstanceWindowControl.NotesBox.Document.ContentEnd).Text;
                CookInstance cookInstance = new(int.Parse(addCookInstanceWindowControl.RatingInteger.Text),
                    notes, Converters.GetTimeSpan(addCookInstanceWindowControl.TimeSpanBox.Text));
                CookInstances.Add(cookInstance);
                Recipe.CookInstances.Add(cookInstance);
            }
        }

        /// <summary>
        /// Selection Logic to add pictures to food
        /// </summary>
        /// <param name="filepath">is null if clicked on, not if drag and dropped</param>
        private void AddPicture(string filepath = null)
        {
            bool deleteOld = false;
            string oldPath = ImagePath;
            // click on image pane
            if (filepath == null)
            {
                string filterString = string.Join(";", acceptedPictureFormats.Select(p => "*" + p));
                Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Food Pictures(" + filterString + ")|" + filterString,
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    Multiselect = false
                };
                if (openFileDialog1.ShowDialog() == true)
                {
                    filepath = openFileDialog1.FileName;
                }
            }

            if (filepath == null)
                return; // if nothing was selected from the dialog box

            // If image already exists, display popup to confirm overriding it
            if (!ImagePath.Contains("TransparentPlus"))
            {
                if (MessageBox.Show("Replace Current Picture?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
                else
                    deleteOld = true;
            }

            string newPath = Path.Combine(Recipe.GetImageFolderPath(), Recipe.Name + "." + filepath.Split(".").Last());
            ImagePath = newPath;
            Picture = new BitmapImage(new Uri(filepath));

            if (deleteOld)
                File.Delete(oldPath);

            File.Copy(filepath, newPath);
        }

        private void SaveEditedRecipe()
        {
            HasChanged = true;
        }

        #endregion
    }
}
