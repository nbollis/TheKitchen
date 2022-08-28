using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;


namespace TheKitchen
{
    public class Recipe
    {
        #region Public Properties
        public string Name { get; set; }
        public int Serves { get; set; }
        public Procedure Procedure { get; set; }
        public List<string> Notes { get; set; }
        public List<Category> Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<CookInstance> CookInstances { get; set; }
        public string Description { get; set; }
        public bool Changed { get; set; }
        public string ImageFilePath { get; set; }


        #endregion

        #region Constructors
        public Recipe()
        {
            Notes = new List<string>();
            Ingredients = new List<Ingredient>();
            CookInstances = new List<CookInstance>();
            Categories = new List<Category>();
            Changed = true;
        }

        #region Saving 

        /// <summary>
        /// Saves a recipe to the filepath provided in xml format
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveRecipe()
        {
            string filepath = Path.Combine(GetRecipeFolderPath(), Name + ".xml");
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
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }
            recipe.Changed = false;
            XmlReaderWriter.WriteToXmlFile(filepath, recipe);
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
