using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnitsNet;

namespace TheKitchen
{
    class Recipe
    {
        private string Name;
        private int Serves;
        private List<string> Procedure;
        private List<string> Notes;
        private List<Ingredient> Ingredients;
        private List<CookInstance> CookInstances;
        private string Description;

        public Recipe()
        {

        }

        /// <summary>
        /// Imports recipes from txt files based upon my standard formatting in OneNote
        /// </summary>
        /// <param name="filepath"></param>
        public Recipe(string filepath)
        {

            Procedure = new List<string>();
            Notes = new List<string>();
            Ingredients = new List<Ingredient>();
            CookInstances = new List<CookInstance>();

            List<string> lines = System.IO.File.ReadAllLines(filepath).Where(p => p.Length > 4 && p != null).ToList(); ;
            Name = lines[0];
            
            List<string> serve = lines.Where(p => p.Contains("Serves ")).ToList();
            int serveIndex = serve[0].IndexOf("Serves");
            int max = serve[0].Length == 8 ? 2 : 3;
            Serves = Int16.Parse(serve[0].Substring(serveIndex + 6, max).Trim());

            int descriptionIndex = lines.IndexOf(lines.Where(p => p.Contains("Description:")).First());
            int notesIndex = lines.IndexOf(lines.Where(p => p.Contains("Notes:")).First());
            int ingredientsIndex = lines.IndexOf(lines.Where(p => p.Contains("Ingredients:")).First());
            int procedureIndex = lines.IndexOf(lines.Where(p => p.Contains("Procedure:")).First());
            int cookedIndex = lines.IndexOf(lines.Where(p => p.Contains("Cooked:")).First());


            Description = lines[descriptionIndex].Replace("Description:", "").Trim();
            for (int i = descriptionIndex + 1; i < notesIndex; i++)
            {
                    Description += lines[i];
            }

            for (int i = notesIndex + 1; i < ingredientsIndex; i++)
            {
                Notes.Add(lines[i]);
            }

            for (int i = ingredientsIndex + 1; i < procedureIndex; i++)
            {
                Ingredient newIngredient = Ingredient.ParseIngredientsFromText(lines[i]);
                if (newIngredient != null) 
                    Ingredients.Add(newIngredient); 
            }

            for (int i = procedureIndex + 1; i < cookedIndex; i++)
            {
                    Procedure.Add(lines[i]);
            }

            for (int i = cookedIndex + 1; i < lines.Count; i++)
            {
                CookInstances.Add(new CookInstance(lines[i]));
            }

            int breakpoint = 0;
    
        }

        
        public static void GetRecipesAsXML(List<Recipe> recipes)
        {
            var xmlfromLINQ = new XElement("Recipes",
                from r in recipes
                select new XElement("Recipe", " Name = " + r.Name, " Serves = " + r.Serves, " Description = " + r.Description,
                new XElement("Ingredients", r.Ingredients),
                new XElement("Procedure", r.Procedure),
                new XElement("Notes", r.Notes),
                new XElement("CookInstance", r.CookInstances)
                ));

            using (StreamWriter writer = new StreamWriter(@"A:\Downloads\RecipesToConvert\aTestRecipeStorage.txt"))
            {
                writer.Write(xmlfromLINQ);  
            }
            
            int breakpoint = 0;
        }

        // Adds a note to the recipe
        // TODO: Make a note class that stores the string and the date it was written on
        public void AddNote(string note)
        {
            Notes.Add(note);
        }



        public override string ToString()
        {
            return this.Name;
        }

    }
}
