using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        #endregion

        #region Public Properties

        public bool HasChanged { get; set; } = false;
        public int BufferThickness { get; set; } = 20;
        public string Name { get; set; }
        public int Serves { get; set; }
        public List<string> Procedure { get; set; }
        public List<string> Notes { get; set; }
        public List<string> Tags { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public List<CookInstance> CookInstances { get; set; }
        public string Description { get; set; }
        public double AverageRating { get; set; }
        public string CookTime { get; set; }
        public string InfoString { get; set; }
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

        #endregion

        #region Constructor

        public RecipeDisplayViewModel(Recipe recipe)
        {
            Name = recipe.Name;
            Serves = recipe.Serves;
            Procedure = recipe.Procedure;
            Notes = recipe.Notes;
            Tags = recipe.Tags;
            CookInstances = recipe.CookInstances;
            Description = recipe.Description;
            AverageRating = recipe.AverageRating;
            CookTime = recipe.CookTime;
            ImagePath = recipe.ImageFilePath;
            Ingredients = new ObservableCollection<Ingredient>(recipe.Ingredients);
            InfoString = recipe.InfoString;

            if (recipe.ImageFilePath != null)
            {
                picture = new BitmapImage(new Uri(recipe.ImageFilePath));
                
            }

            AddToMealPrepCommand = new RelayCommand(() => AddToMealPrep());
            DownloadRecipeCommand = new RelayCommand(() => DownloadRecipe());
            PrintRecipeCommand = new RelayCommand(() => PrintRecipe());
            EditRecipeCommand = new RelayCommand(() => EditRecipe());
            CommentRecipeComamnd = new RelayCommand(() => CommentRecipe());
            AddPictureCommand = new RelayCommand(() => AddPicture());
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

        #region Private Helpers

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
            EditRecipeWindowControl recipeEditer = new();
            EditRecipeViewModel viewModel = new(recipeEditer);
            viewModel.Name = Name;
            recipeEditer.DataContext = viewModel;
            recipeEditer.EditRecipeContentGrid.DataContext = this;
            recipeEditer.ShowDialog();
        }

        private void CommentRecipe()
        {

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

            string newPath = Path.Combine(Recipe.GetImageFolderPath(), Name + "." + filepath.Split(".").Last());
            ImagePath = newPath;
            Picture = new BitmapImage(new Uri(filepath));

            if (deleteOld)
                File.Delete(oldPath);

            File.Copy(filepath, newPath);            
        }

        #endregion
    }
}
