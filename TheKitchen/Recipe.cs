using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    class Recipe
    {
        private readonly string Name;
        private readonly int Serves;
        private readonly List<Food> Ingredients;
        private readonly double[] Amounts;
        private readonly string[] Units;
        private readonly string[] Category;
        private readonly string[] Steps;
        private readonly List<string> Notes;
        //private List<dates> WhenCooked;


        public void PrintRecipe()
        {

        }

        // Adds a note to the recipe
        // TODO: Make a note class that stores the string and the date it was written on
        public void AddNote(string note)
        {
            Notes.Add(note);
        }



        public override string ToString()
        {
            return base.ToString();
        }
    }
}
