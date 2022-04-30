using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace TheKitchen
{
    public class Recipe
    {
        public string Name { get; set; }
        public int Serves { get; set; }
        public List<string> Procedure { get; set; }
        public List<string> Notes { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<CookInstance> CookInstances { get; set; }
        public string Description { get; set; }
        public bool Changed { get; set; }

        public Recipe()
        {

        }

        /// <summary>
        /// Imports recipes from txt files based upon my standard formatting in OneNote
        /// </summary>
        /// <param name="filepath"></param>
        public Recipe(List<string> lines)
        {

            Procedure = new List<string>();
            Notes = new List<string>();
            Ingredients = new List<Ingredient>();
            CookInstances = new List<CookInstance>();
            Changed = true;

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
    
        }

        

        public static void SaveRecipe(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                SaveRecipe(recipe);
            }
        }

        public static void SaveRecipe(Recipe recipe)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes", recipe.Name + ".xml");
            bool exists = File.Exists(filepath); 
            if (recipe.Changed)
            {
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Close();
                }
                recipe.Changed = false;
                XmlReaderWriter.WriteToXmlFile(filepath, recipe);
            }
                
        }



        /// <summary>
        /// Loads in All recipes stored in xml format in the Datafiles/Recipes folder
        /// </summary>
        /// <returns></returns>
        public static List<Recipe> LoadRecipes(string folderPath = null)
        {
            List<Recipe> recipes = new List<Recipe> ();
            if (folderPath == null)
            {
                folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes");
            }

            foreach (string file in Directory.EnumerateFiles(folderPath))
            {
                if (file.EndsWith(".xml"))
                {
                    recipes.Add(XmlReaderWriter.ReadFromXmlFile<Recipe>(file));
                }
                else if (file.EndsWith(".txt"))
                {
                    recipes.Add(new Recipe(System.IO.File.ReadAllLines(file).Where(p => p.Length > 4 && p != null).ToList()));
                }
            }
            return recipes;
        }


        public override string ToString()
        {
            return this.Name;
        }

    }
}
