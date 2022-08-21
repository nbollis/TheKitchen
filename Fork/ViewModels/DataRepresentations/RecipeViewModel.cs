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
using ForkDataHandling;
using TheKitchen;

namespace Fork
{
    public class RecipeViewModel : BaseViewModel, IFileDragDropTarget
    {

        #region Private Members

        private string name;
        private int _serves;
        private ObservableCollection<string> _procedure;
        private ObservableCollection<string> _notes;
        private ObservableCollection<CategoryViewModel> _categories;
        private ObservableCollection<Ingredient> _ingredients;
        private ObservableCollection<CookInstance> _cookInstances;
        private string _description;
        private double _averageRating;
        private string _cookTime;
        private int _timesCooked;
        private static List<Category> _possibleCategories = ForkGlobalData.AllCategories;
        private Recipe recipe;
    

        #endregion

        #region Public Properties

        public Recipe Recipe
        {
            get { return recipe; }
            set { recipe = value; OnPropertyChanged(nameof(Recipe));}
        }
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
        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }   
            set {_categories = value;
                Recipe.Categories = value.Select(p => p.Category).ToList();
                OnPropertyChanged(nameof(Categories)); }
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
        public int TimesCooked
        {
            get { return _timesCooked; }
            set { _timesCooked = value; 
                OnPropertyChanged(nameof(TimesCooked)); }
        }

        // ViewModel specific
        public bool HasChanged { get; set; } = false;
        public bool NameHasChanged { get; set; } = false;
        public int BufferThickness { get; set; } = 10;
        public bool IsSelected { get; set; }
        public BitmapImage? Picture 
        { 
            get { return Recipe.ImageFilePath == null ? null : new BitmapImage(new Uri(Recipe.ImageFilePath)); }  
        }
        public List<Category> PossibleCateogories
        {
            get { return _possibleCategories; }
            set { _possibleCategories = value; OnPropertyChanged(nameof(PossibleCateogories)); }
        }

        #endregion

        #region Commands

            public ICommand AddToMealPrepCommand { get; set; }
            public ICommand DownloadRecipeCommand { get; set; }
            public ICommand PrintRecipeCommand { get; set; }
            public ICommand EditRecipeCommand { get; set; }
            public ICommand CommentRecipeComamnd { get; set; }
            public ICommand AddPictureCommand { get; set; }
            public ICommand SaveEditedRecipeCommand { get; set; }
            public ICommand RecipeSelectedCommand { get; set; }
            public ICommand AddCategoryCommand { get; set; }
            public ICommand RemoveCategoryCommand { get; set; }
            public ICommand CreateCategoryCommand { get; set; }

            public static event EventHandler<ListItemSelectedEventArgs>? ItemSelected;

            #endregion

        #region Constructor

        public RecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;
            Name = recipe.Name;
            Serves = recipe.Serves;
            Procedure = new ObservableCollection<string>(recipe.Procedure);
            Notes = new ObservableCollection<string>(recipe.Notes);
            Categories = new ObservableCollection<CategoryViewModel>(CategoryViewModel.GetViewModels(recipe.Categories));
            Ingredients = new ObservableCollection<Ingredient>(recipe.Ingredients);
            CookInstances = new ObservableCollection<CookInstance>(recipe.CookInstances);
            Description = recipe.Description;

            CalculateAverageValues();

            AddToMealPrepCommand = new RelayCommand(AddToMealPrep);
            DownloadRecipeCommand = new RelayCommand(DownloadRecipe);
            PrintRecipeCommand = new RelayCommand(PrintRecipe);
            EditRecipeCommand = new RelayCommand(EditRecipe);
            CommentRecipeComamnd = new RelayCommand(CommentRecipe);
            AddPictureCommand = new RelayCommand(() => AddPicture());
            SaveEditedRecipeCommand = new RelayCommand(SaveEditedRecipe);
            RecipeSelectedCommand = new RelayCommand(SelectRecipe);
            AddCategoryCommand = new DelegateCommand((param) => AddCategory(param));
            RemoveCategoryCommand = new DelegateCommand((param) => RemoveCategory(param));
            CreateCategoryCommand = new RelayCommand(OpenCategoryWindow);
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
            ForkGlobalData.SaveAllCategoriesList();
        }

        private void EditRecipe()
        {
            EditRecipeWindowView editRecipeWindowView = new(this);
            WindowViewModel windowViewModel = new(editRecipeWindowView);
            editRecipeWindowView.DataContext = windowViewModel;
            editRecipeWindowView.ShowDialog();
            CalculateAverageValues();
        }

        private void CommentRecipe()
        {
            AddCookInstanceWindowView addCookInstanceWindowView = new(this);
            WindowViewModel windowViewModel = new(addCookInstanceWindowView);
            addCookInstanceWindowView.DataContext = windowViewModel;
            addCookInstanceWindowView.ShowDialog();

            if (addCookInstanceWindowView.DialogResult == true)
            {
                HasChanged = true;
                string notes = new TextRange(addCookInstanceWindowView.NotesBox.Document.ContentStart, addCookInstanceWindowView.NotesBox.Document.ContentEnd).Text;
                CookInstance cookInstance = new(int.Parse(addCookInstanceWindowView.RatingInteger.Text),
                    notes, Converters.GetTimeSpan(addCookInstanceWindowView.TimeSpanBox.Text));
                CookInstances.Add(cookInstance);
                Recipe.CookInstances.Add(cookInstance);
            }
            CalculateAverageValues();
        }

        /// <summary>
        /// Selection Logic to add pictures to food
        /// </summary>
        /// <param name="filepath">is null if clicked on, not if drag and dropped</param>
        private void AddPicture(string filepath = null)
        {
            bool deleteOld = false;
            string oldPath = Recipe.ImageFilePath;
            // click on image pane
            if (filepath == null)
            {
                string filterString = string.Join(";", ForkGlobalData.AcceptedPictureFormats.Select(p => "*" + p));
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
            if (!Recipe.ImageFilePath.Contains("TransparentPlus"))
            {
                if (MessageBox.Show("Replace Current Picture?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
                else
                    deleteOld = true;
            }

            string newPath = Path.Combine(Recipe.GetImageFolderPath(), Recipe.Name + "." + filepath.Split(".").Last());
            Recipe.ImageFilePath = newPath;
            OnPropertyChanged(nameof(Picture));

            if (deleteOld)
                File.Delete(oldPath);

            File.Copy(filepath, newPath);
        }

        private void SaveEditedRecipe()
        {
            HasChanged = true;
            SaveRecipeVMChanges();
        }

        private void SelectRecipe()
        {
            IsSelected = true;
            ListItemSelectedEventArgs e = new ListItemSelectedEventArgs(Name);
            ItemSelected?.Invoke(this, e);
        }

        private void AddCategory(object param)
        {
            Category? category = (Category)param;
            if (category != null )
            {
                Categories.Add(new CategoryViewModel(category));
                Recipe.Categories.Add(category);
            }
            else
            {
                MessageBox.Show("Unexpected error when adding category to recipe");
            }
        }

        private void RemoveCategory(object param)
        {
            Category? category = (Category)param;
            if (category != null)
            {
                if (ForkGlobalData.AllCategories.Any(p => p.Equals(category)))
                {
                    if (Categories.Any(p => p.Name.Equals(category.Name)))
                    {
                        Categories.Remove(Categories.First(p => p.Category == category));
                        Recipe.Categories.Remove(Recipe.Categories.First(p => p == category));
                    }
                    else
                    {
                        MessageBox.Show("Recipe is not in this category");
                    }
                }
                else
                {
                    MessageBox.Show("Category to remove does not exist");
                }
            }
            else
            {
                MessageBox.Show("Category to remove not selected");
            }
        }

        private void OpenCategoryWindow()
        {
            EditCategoriesViewModel editCategoriesViewModel = new EditCategoriesViewModel();
            AddCategoryWindowView addCategoryWindowView = new(editCategoriesViewModel);
            WindowViewModel windowViewModel = new(addCategoryWindowView);
            addCategoryWindowView.DataContext = windowViewModel;
            addCategoryWindowView.ShowDialog();
            _possibleCategories = ForkGlobalData.AllCategories;
            OnPropertyChanged(nameof(PossibleCateogories));
        }

        #endregion

        /// <summary>
        /// Saves the recipe and accounts for a potential name change
        /// </summary>
        public void SaveRecipeVMChanges()
        {
            if (NameHasChanged)
            {
                // delete current xml
                string filepath = Path.Combine(Recipe.GetRecipeFolderPath(), Recipe.Name + ".xml");
                bool exists = File.Exists(filepath);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                Recipe.Name = Name;
            }

            Recipe.SaveRecipe();
        }
    }
}
