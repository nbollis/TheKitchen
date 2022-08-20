using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheKitchen;
using System.Windows.Controls;
using System.Windows;

namespace Fork
{
    public class EditCategoriesViewModel : BaseViewModel, IListContainer
    {

        #region Private Members

        private ObservableCollection<CategoryViewModel> allCategories;

        #endregion

        #region Public Properties

        public ObservableCollection<CategoryViewModel> AllCategories
        {
            get { return allCategories; }
            set { allCategories = value; OnPropertyChanged(nameof(AllCategories)); }
        }

        public CategoryViewModel SelectedCategory { get; set; }

        #endregion

        #region Commands
        public ICommand CreateNewCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }
        public ICommand SaveCategoryCommand { get; set; }

        #endregion

        #region Constructor

        public EditCategoriesViewModel()
        {
            allCategories = new ObservableCollection<CategoryViewModel>();
            SetAllCategoriesToStaticValue();
            CreateNewCategoryCommand = new RelayCommand(() => CreateNewCategory());
            DeleteCategoryCommand = new RelayCommand(() => DeleteCategory());
            SaveCategoryCommand = new RelayCommand(() => SaveCategory());
        }

        #endregion

        #region Private Helpers

        private void SetAllCategoriesToStaticValue()
        {
            foreach (var category in ForkGlobalData.AllCategories)
            {
                allCategories.Add(new CategoryViewModel(category, this));
            }
        }


        private void CreateNewCategory()
        {
            CategoryViewModel newCategory = new()
            {
                Category = new Category(),
                Name = "Default",
                RGB = "#ffffff",
                Description = "",
            };
            allCategories.Add(newCategory);
            ListItemSelected(newCategory.Name);
            OnPropertyChanged(nameof(AllCategories));
        }

        private void DeleteCategory()
        {
            MessageBoxButton btn = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show($"Are you sure you would like to delete {SelectedCategory.Name}?", "Delete Category", btn, image);

            if (result == MessageBoxResult.Yes)
            {
                AllCategories.Remove(SelectedCategory);
                SaveCategory();
            }            
        }

        private void SaveCategory()
        {
            ForkGlobalData.AllCategories = AllCategories.Select(p => p.Category).ToList();
            ForkGlobalData.SaveAllCategoriesList();
            ForkGlobalData.LoadAllCategoriesList();
        }

        #endregion

        public void ListItemSelected(object obj, ListItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ListItemSelected(object obj)
        {
            if (SelectedCategory != null)
            {
                SelectedCategory.IsSelected = false;
            }
            SelectedCategory = AllCategories.First(p => p.Name.Equals(obj));
            SelectedCategory.IsSelected = true;
        }
    }
}
