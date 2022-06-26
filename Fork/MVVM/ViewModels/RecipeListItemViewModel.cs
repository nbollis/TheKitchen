using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace Fork
{
    public class RecipeListItemViewModel : BaseViewModel
    {
        #region Private

        private string _name;
        private ObservableCollection<TagViewModel> _tags;
        private string imageFilePath;

        #endregion

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public ObservableCollection<TagViewModel> Tags
        {
            get { return _tags; }
            set { _tags = value; OnPropertyChanged(nameof(Tags)); }
        }

        public BitmapImage Picture 
        {
            get { return new BitmapImage(new Uri(imageFilePath)); }  
        }
        public bool IsSelected { get; set; }   

        #endregion

        #region Public Commands

        public ICommand OpenRecipeCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for real time data
        /// </summary>
        /// <param name="recipe"></param>
        public RecipeListItemViewModel(Recipe recipe)
        {
            _name = recipe.Name;
            IsSelected = false;
            Tags = new ObservableCollection<TagViewModel>();
            imageFilePath = recipe.ImageFilePath;
            foreach (var tags in recipe.Tags)
            {
                Tags.Add(new TagViewModel());
            }

            OpenRecipeCommand = new RelayCommand(OpenRecipe);
        }

        /// <summary>
        /// Constructor for design time data
        /// </summary>
        public RecipeListItemViewModel()
        {
            Name = "Tacos";
            Tags = new ObservableCollection<TagViewModel>();
        }


        #endregion

        #region Command Methods 

        public void OpenRecipe()
        {
            IsSelected = true;
        }

        #endregion
    }

}
