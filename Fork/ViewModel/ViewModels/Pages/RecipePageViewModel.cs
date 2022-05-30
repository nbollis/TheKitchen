﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheKitchen;

namespace Fork
{
    public class RecipePageViewModel : BaseViewModel
    {
        private ObservableCollection<Recipe> _Recipes;
        private RecipeListViewModel _RecipeListViewModel;
        private RecipeDisplayViewModel _RecipeDisplayViewModel;

        #region Public Properties

        public ObservableCollection<Recipe> Recipes
        {
            get { return _Recipes; }
            set
            {
                _Recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }

        }

        public RecipeListViewModel RecipeListViewModel
        {
            get { return _RecipeListViewModel; }
            set
            {
                _RecipeListViewModel = value;
                OnPropertyChanged(nameof(RecipeListViewModel));
            }
        }

        public RecipeDisplayViewModel RecipeDisplayViewModel
        {
            get { return _RecipeDisplayViewModel; }
            set
            {
                _RecipeDisplayViewModel = value;
                OnPropertyChanged(nameof(RecipeDisplayViewModel));
            }
        }

        public static int BufferThickness { get; set; } = 20;

        #endregion

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand ForwardCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ChangeViewCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand RecipeSelectedCommand { get; set; }

        #endregion

        #region Constructor

        public RecipePageViewModel()
        {
            // Initialize Fields
            _Recipes = new ObservableCollection<Recipe>(Recipe.LoadRecipes());
            _RecipeListViewModel = new RecipeListViewModel(this);


            // Declare Commands
            BackCommand = new RelayCommand(() => GoBack());
            ForwardCommand = new RelayCommand(() => GoForward());
            SearchCommand = new RelayCommand(() => Search());
            ChangeViewCommand = new RelayCommand(() => ChangeView());
            SettingsCommand = new RelayCommand(() => Settings());
            RecipeSelectedCommand = new DelegateCommand(param => RecipeSelected(param));
        }

        #endregion

        #region Command Methods
        private void GoBack()
        {
            int breakpoint = 0;
        }

        private void GoForward()
        {

        }

        private void Search()
        {

        }

        private void ChangeView()
        {

        }

        private void Settings()
        {

        }

        /// <summary>
        /// Event when a Recipe is Selected
        /// </summary>
        /// <param name="obj"></param>
        public void RecipeSelected(object obj)
        {
            Recipe recipe = Recipes.First(p => p.Name.Equals(obj));
            // make changes on the left side of the screen
            if (RecipeListViewModel.SelectedItem != null)
            {
                RecipeListViewModel.SelectedItem.IsSelected = false;
            }
            RecipeListViewModel.SelectedItem = RecipeListViewModel.RecipeList.First(p => p.Name.Equals(obj));
            RecipeListViewModel.SelectedItem.IsSelected = true;

            if (RecipeDisplayViewModel != null && RecipeDisplayViewModel.HasChanged)
                SaveRecipeVMChanges();

            // make changes on the right side of the screen
            RecipeDisplayViewModel = new RecipeDisplayViewModel(recipe);
        }

        #endregion


        public void OnClosing()
        {
            if (RecipeDisplayViewModel != null && RecipeDisplayViewModel.HasChanged)
                SaveRecipeVMChanges();

            foreach (var recipe in Recipes.Where(p => p.Changed))
            {
                Recipe.SaveRecipe(recipe);
            }
        }

        /// <summary>
        /// Saves the changes to the curently displayed Recipe to the list of recipes
        /// </summary>
        public void SaveRecipeVMChanges()
        {
            Recipe recipe = new Recipe();
            recipe.Name = RecipeDisplayViewModel.Name;
            recipe.Serves = RecipeDisplayViewModel.Serves;
            recipe.Procedure = RecipeDisplayViewModel.Procedure;
            recipe.Notes = RecipeDisplayViewModel.Notes;
            recipe.Tags = RecipeDisplayViewModel.Tags;
            recipe.Ingredients = RecipeDisplayViewModel.Ingredients.ToList();
            recipe.CookInstances = RecipeDisplayViewModel.CookInstances;
            recipe.Description = RecipeDisplayViewModel.Description;
            recipe.Changed = RecipeDisplayViewModel.HasChanged;
            recipe.AverageRating = RecipeDisplayViewModel.AverageRating;
            recipe.ImageFilePath = RecipeDisplayViewModel.ImagePath;
            recipe.CookTime = RecipeDisplayViewModel.CookTime;
            recipe.InfoString = RecipeDisplayViewModel.InfoString;
            int index = Recipes.IndexOf(Recipes.First(p => p.Name.Equals(recipe.Name)));
            Recipes[index] = recipe;
        }

        #region Private Helpers

        #endregion
    }
}
