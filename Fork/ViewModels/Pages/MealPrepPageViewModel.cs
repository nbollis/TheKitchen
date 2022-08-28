using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public class MealPrepPageViewModel : BaseViewModel, IPage, IListContainer
    {
        #region Private Members

        private ObservableCollection<MealPrepViewModel> mealPreps;
        private MealPrepViewModel selectedMealPrepViewModel;
        private Stopwatch stopwatch;

        #endregion

        #region Public Properties
        public ApplicationPage ApplicationPage => ApplicationPage.MealPrep;

        public ObservableCollection<MealPrepViewModel> MealPreps
        {
            get => mealPreps;
            set { mealPreps = value; OnPropertyChanged(nameof(MealPreps)); }
        }

        public MealPrepViewModel SelectedMealPrepViewModel
        {
            get => selectedMealPrepViewModel;
            set { selectedMealPrepViewModel = value; OnPropertyChanged(nameof(SelectedMealPrepViewModel)); }
        }

        #endregion

        #region Commands

        #endregion

        #region Constructor

        public MealPrepPageViewModel()
        {
            mealPreps = new ObservableCollection<MealPrepViewModel>();
            selectedMealPrepViewModel = mealPreps.FirstOrDefault();
            stopwatch = new();
        }

        #endregion

        #region Helpers
        public void OnClosing()
        {
            throw new NotImplementedException();
        }

        public void ListItemSelected(object obj, ListItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ListItemSelected(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
