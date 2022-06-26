using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;


namespace TheKitchen
{
    public class Recipe : IRecipe
    {
        #region Public Properties
        public string Name { get; set; }
        public int Serves { get; set; }
        public List<string> Procedure { get; set; }
        public List<string> Notes { get; set; }
        public List<string> Tags { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<CookInstance> CookInstances { get; set; }
        public string Description { get; set; }
        public bool Changed { get; set; }
        public string ImageFilePath { get; set; }


        #endregion

        #region Constructors
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

        #region Saving 

        /// <summary>
        /// Saves a recipe to the filepath provided in xml format
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveRecipe()
        {
            string filepath = Path.Combine(GetRecipeFolderPath(), Name + ".xml");
            bool exists = File.Exists(filepath);
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }
            Changed = false;
            XmlReaderWriter.WriteToXmlFile(filepath, this);
        }
        #endregion


        #endregion

        #region Calculation Methods


        #endregion

        #region Static Saving and Loading

        /// <summary>
        /// Saves a list of recipe objects in xml format
        /// </summary>
        /// <param name="recipes"></param>
        public static void SaveRecipe(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                SaveRecipe(recipe);
            }
        }

        /// <summary>
        /// Saves a recipe object in xml format
        /// </summary>
        /// <param name="recipe"></param>
        public static void SaveRecipe(Recipe recipe)
        {
            string filepath = Path.Combine(GetRecipeFolderPath(), recipe.Name + ".xml");
            bool exists = File.Exists(filepath);
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }
            recipe.Changed = false;
            XmlReaderWriter.WriteToXmlFile(filepath, recipe);
        }

        /// <summary>
        /// Loads in All recipes stored in xml format in the Datafiles/Recipes folder as well as txt format if the folderpath is specified
        /// </summary>
        /// <returns></returns>
        public static List<Recipe> LoadRecipes(string folderPath = null)
        {
            // loading them in from xml
            List<Recipe> recipes = new List<Recipe>();
            if (folderPath == null)
            {
                folderPath = Path.Combine(GetRecipeFolderPath());
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


            // adjusting anything on load
            foreach (var recipe in recipes)
            {
                // set the image path
                if (recipe.ImageFilePath == null)
                {
                    recipe.ImageFilePath = Path.Combine(GetImageFolderPath(), @"TransparentPlus.png");
                }

               
            }

            // toggle this when you want to edit all recipes at once
            //SaveRecipe(recipes);

            return recipes;
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }

        #region Static Methods

        public static string GetImageFolderPath()
        { 
            string imageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\RecipePictures");
            if (imageFolderPath.Contains(@"bin\Debug\net6.0-windows\"))
                imageFolderPath = imageFolderPath.Replace(@"\Fork\bin\Debug\net6.0-windows\", @"\TheKitchen\");
            return imageFolderPath;
        }

        public static string GetRecipeFolderPath()
        {
            string imageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes");
            if (imageFolderPath.Contains(@"bin\Debug\net6.0-windows\"))
                imageFolderPath = imageFolderPath.Replace(@"\Fork\bin\Debug\net6.0-windows\", @"\TheKitchen\");
            return imageFolderPath;
        }

        #endregion

    }
}
