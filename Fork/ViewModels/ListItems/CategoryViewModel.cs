
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheKitchen;

namespace Fork
{
    // TODO implement a command so that when you click on the tag, it adds
    // that to the filter and right click takes it away, possibly could have
    // them appear to be pushed buttons, or turn them into buttons later
    public class CategoryViewModel : BaseViewModel
    {
        #region Private Members

        private string categoryName;
        private string rGB;
        private string categoryDescription;
        private bool isSelected;
        private Category category;

        #endregion

        #region Public Properties
        public Category Category { 
            get { return category; }
            set { category = value; OnPropertyChanged(nameof(Category)); }
        }
        public string Name
        {
            get { return categoryName; }
            set { categoryName = value; Category.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string RGB
        {
            get { return rGB; }
            set { rGB = value; Category.RGB = value; OnPropertyChanged(nameof(RGB)); }
        }

        public string Description
        {
            get { return categoryDescription; }
            set { categoryDescription = value; Category.Description = value; OnPropertyChanged(nameof(Description)); }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        public IListContainer? Ancestor { get; set; }

        #endregion

        #region Commands

        public ICommand ItemSelectedCommand { get; set; }

        #endregion

        #region Constructor

        public CategoryViewModel(Category category, IListContainer ancestor = null)
        {
            Category = category;
            categoryName = category.Name;
            rGB = category.RGB;
            categoryDescription = category.Description;
            if (ancestor != null)
            {
                Ancestor = ancestor;
                ItemSelectedCommand = new DelegateCommand((param) => Ancestor.ListItemSelected(param));
            }
        }

        #endregion

        #region Private Helpers

        #endregion


        /// <summary>
        /// Creates category view modesl from a list of strings representing their names
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static ObservableCollection<CategoryViewModel> GetViewModels(List<Category> categories)
        {
            ObservableCollection<CategoryViewModel> viewModels = new ObservableCollection<CategoryViewModel>();
            foreach (Category category in categories)
            {
                viewModels.Add(new CategoryViewModel(category));
            }
            return viewModels;
        }

      

    }
}
