using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace ForkIO
{
    public static class RecipeParser
    {
        public static List<Recipe> ParseRecipes(string[] filepaths, out List<string> errors)
        {
            List<Recipe> recipes = new List<Recipe>();
            errors = new List<string>();

            foreach (var path in filepaths)
            {
                TryParseRecipe(path, out Recipe recipe, out string error);

                if (error != null)
                {
                    errors.Add(error);
                }
                if (recipe != null)
                {
                    recipes.Add(recipe);
                }
            }
            return recipes;
        }
        public static List<Recipe> ParseRecipes(string path, out List<string> errors)
        {
            if (Directory.Exists(path))
                return ParseRecipes(Directory.GetFiles(path),out errors);
            else if (File.Exists(path))
                return ParseRecipes(new string[] { path }, out errors);
            else
                throw new ArgumentException("ParseRecipes Failed: single path parameter");
        }

        public static Recipe ParseRecipe(string filepath, FileType filetype)
        {
            switch (filetype)
            {
                case FileType.OneNoteTxt:
                    return ParseRecipeFromOneNoteTxt(filepath);
                    break;
                case FileType.XmlSaved:
                    return XmlReaderWriter.ReadFromXmlFile<Recipe>(filepath);
                    break;
            }

            throw new ArgumentException("Failed to parse recipe");
        }

        public static bool TryParseRecipe(string filepath, out Recipe recipe, out string error)
        {
            try
            {
                recipe = ParseRecipe(filepath, ParseFileType(filepath));
                error = null;
                return true;
            }
            catch (Exception e)
            {
                recipe = null;
                error = e.Message;
                return false;
            }
        }



        #region Specific Parsing Methods

        public static Recipe ParseRecipeFromOneNoteTxt(string filepath)
        {
            Recipe recipe = new Recipe();
            List<string> lines = File.ReadAllLines(filepath).Where(p => p.Length > 4 && p != null).ToList();

            recipe.Name = lines[0];
            List<string> serve = lines.Where(p => p.Contains("Serves ")).ToList();
            int serveIndex = serve[0].IndexOf("Serves");
            int max = serve[0].Length == 8 ? 2 : 3;
            recipe.Serves = Int16.Parse(serve[0].Substring(serveIndex + 6, max).Trim());

            int descriptionIndex = lines.IndexOf(lines.Where(p => p.Contains("Description:")).First());
            int notesIndex = lines.IndexOf(lines.Where(p => p.Contains("Notes:")).First());
            int ingredientsIndex = lines.IndexOf(lines.Where(p => p.Contains("Ingredients:")).First());
            int procedureIndex = lines.IndexOf(lines.Where(p => p.Contains("Procedure:")).First());
            int cookedIndex = lines.IndexOf(lines.Where(p => p.Contains("Cooked:")).First());


            recipe.Description = lines[descriptionIndex].Replace("Description:", "").Trim();
            for (int i = descriptionIndex + 1; i < notesIndex; i++)
            {
                recipe.Description += lines[i];
            }

            for (int i = notesIndex + 1; i < ingredientsIndex; i++)
            {
                recipe.Notes.Add(lines[i]);
            }

            for (int i = ingredientsIndex + 1; i < procedureIndex; i++)
            {
                Ingredient newIngredient = Ingredient.ParseIngredientsFromText(lines[i]);
                if (newIngredient != null)
                    recipe.Ingredients.Add(newIngredient);
            }

            for (int i = procedureIndex + 1; i < cookedIndex; i++)
            {
                recipe.Procedure.Add(lines[i]);
            }

            for (int i = cookedIndex + 1; i < lines.Count; i++)
            {
                recipe.CookInstances.Add(new CookInstance(lines[i]));
            }

            recipe.ImageFilePath = Path.Combine(Recipe.GetImageFolderPath(), @"TransparentPlus.png");

            return recipe;
        }

        #endregion

        public static FileType ParseFileType(string filepath)
        {
            string extension = Path.GetExtension(filepath);
            switch (extension)
            {
                case ".txt":
                    return FileType.OneNoteTxt; break;
                case ".xml":
                    return FileType.XmlSaved; break;
            }

            throw new ArgumentException($"File type {extension} not yet supported");
        }

        public static bool TryParseFileType(string filepath, out FileType? filetype, out string error)
        {
            try
            {
                filetype = ParseFileType(filepath);
                error = null;
                return true;
            }
            catch (ArgumentException e)
            {
                error = e.Message;
                filetype = null;
                return false;
            }
        }


    }
}
