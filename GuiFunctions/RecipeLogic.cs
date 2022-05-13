using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TheKitchen;

namespace GuiFunctions
{
    public class RecipeLogic
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public object ThreadLocker;
        public Engine Engine;

        public RecipeLogic()
        {
            Engine = new Engine();
            ThreadLocker = new object();

            Engine.LoadAll();
            Recipes = new ObservableCollection<Recipe>();
            foreach (var recipe in Engine.Recipes)
            {
                Recipes.Add(recipe);
            }
        }
    }
}
