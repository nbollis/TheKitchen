using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace TheKitchen
{
    public static class RecipeParser
    {
        public static List<Recipe> ParseRecipes(string[] filepaths, out List<string> errors)
        {
            List<Recipe> recipes = new List<Recipe>();
            errors = new List<string>();

            foreach (var path in filepaths)
            {
                TryParseRecipe(path, out Recipe recipe, out errors);

                if (errors != null)
                {
                    errors.AddRange(errors);
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

        public static bool TryParseRecipe(string filepath, out Recipe recipe, out List<string> errors)
        {
            errors = new List<string>();
            recipe = null;
            try
            {
                switch (ParseFileType(filepath))
                {
                    case FileType.OneNoteTxt:
                        recipe = ParseRecipeFromOneNoteTxt(filepath, out errors);
                        break;

                    case FileType.XmlSaved:
                        recipe = XmlReaderWriter.ReadFromXmlFile<Recipe>(filepath);
                        errors = null;
                        break;

                    case FileType.pdf:
                        recipe = ParseRecipeFromGeneralText(filepath, out errors);
                        break;
                }

                
                return true;
            }
            catch (Exception)
            {
                recipe = null;
                return false;
            }
        }



        #region Specific Parsing Methods

        // TODO: Refactor this to use the better recipe constructor
        public static Recipe ParseRecipeFromOneNoteTxt(string filepath, out List<string> errors)
        {
            errors = new List<string>();
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
                if (Ingredient.TryParseIngredientFromTxt(lines[i], out Ingredient ingredient, out string error))
                {
                    recipe.Ingredients.Add(ingredient);
                }
                else
                {
                    recipe.Ingredients.Add(new Ingredient());
                    errors.Add(error);
                }
            }

            for (int i = procedureIndex + 1; i < cookedIndex; i++)
            {
                recipe.Procedure.Add(lines[i]);
            }

            for (int i = cookedIndex + 1; i < lines.Count; i++)
            {
                if (CookInstance.TryParseCookInstanceFromTxt(lines[i], out CookInstance cookInstance, out string error))
                {
                    recipe.CookInstances.Add(cookInstance);
                }
                else
                {
                    recipe.CookInstances.Add(new CookInstance());
                    errors.Add(error);
                }
            }

            recipe.ImageFilePath = Path.Combine(Recipe.GetImageFolderPath(), @"TransparentPlus.png");

            return recipe;
        }

        public static Recipe ParseRecipeFromGeneralText(string filepath, out List<string> errors)
        {
            Recipe recipe = new Recipe();
            errors = new List<string>();
            string text = PdfExtracter.pdfText(filepath);

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
                case ".pdf":
                    return FileType.pdf; break;
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
