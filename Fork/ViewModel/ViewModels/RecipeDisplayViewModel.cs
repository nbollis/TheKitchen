using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace Fork
{
    public class RecipeDisplayViewModel : BaseViewModel, IFileDragDropTarget
    {

        #region Private Members

        private BitmapImage picture;

        #endregion

        #region Public Properties

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
        public BitmapImage Picture { get { return picture; }  }
        public string ImagePath { get; set; }

        #endregion

        #region Commands

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




            string cooktime = CookInstances.Any() ? CookInstances.Average(p => p.CookTime) / 60 + "h " + 
                CookInstances.Average(p => p.CookTime) % 60 + "m" : "n/a";
            string rating = CookInstances.Any() ? CookInstances.Average(p => p.rating).ToString() : "n/a";
            InfoString = "Serves: " + Serves + "      Rating: " + rating + "     Cook Time: " + cooktime + "     Times Cooked: " + CookInstances.Count();

            if (recipe.ImageFilePath != null)
            {
                picture = new BitmapImage(new Uri(recipe.ImageFilePath));
                
            }
                
        }

        public RecipeDisplayViewModel()
        {

        }

        public void OnFileDrop(string[] filepaths)
        {
            if (filepaths.Count() > 1)
            {
                MessageBox.Show("Only One Picture Allowed");
                return;
            }
            ImagePath = filepaths[0];
        }

        #endregion

        #region Private Helpers

        #endregion
    }
}
