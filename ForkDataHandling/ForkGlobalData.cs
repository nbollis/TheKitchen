using System.Text.RegularExpressions;
using TheKitchen;

namespace ForkDataHandling
{
    public class ForkGlobalData 
    {
        #region Static Properties

        public static List<string> AcceptedPictureFormats = new List<string> { ".jpg", ".png", ".jpeg" };
        public static List<string> AcceptedRecipeUploadFormats = new List<string> { ".txt" };
        
        public static List<Category> AllCategories { get; set; }
        public static List<Recipe> AllRecipes { get; set; }
        public static Dictionary<string, Recipe> AllRecipesDict { get; set; }
        #endregion


        static ForkGlobalData()
        {
            LoadAllCategoriesList();
            LoadAllRecipes();
        }

        #region Singleton for the view to reference

        public ForkGlobalData Instance => new ForkGlobalData();
        public ForkGlobalData() {}

        #endregion


        #region IO of static data
        public static void LoadAllCategoriesList()
        {
            List<Category> categories = new();
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\CategoryColorPairs.xml");
            categories = XmlReaderWriter.ReadFromXmlFile<List<Category>> (filepath);
            AllCategories = categories;
        }

        public static void SaveAllCategoriesList()
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\CategoryColorPairs.xml");
            if (!File.Exists(filepath))
                File.Create(filepath).Close();
            XmlReaderWriter.WriteToXmlFile(filepath, AllCategories);
        }
        private static void LoadAllRecipes()
        {
            AllRecipes = RecipeParser.ParseRecipes(Recipe.GetRecipeFolderPath(), out List<string> errors);
            AllRecipesDict = AllRecipes.ToDictionary(p => p.Name, p => p);
        }

        #endregion

        #region Value Checks

        public static bool CheckIfValueIsInteger(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        #endregion

        



        

       
    }
}
