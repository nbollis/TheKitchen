using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TheKitchen;

namespace Fork
{
    public class ForkGlobalData : BaseViewModel, IListContainer
    {
        #region Static Properties

        public static List<string> AcceptedPictureFormats = new List<string> { ".jpg", ".png", ".jpeg" };
        public static List<string> AcceptedRecipeUploadFormats = new List<string> { ".txt" };

        public static List<Category> AllCategories { get; set; }

        #endregion


        static ForkGlobalData()
        {
            LoadAllCategoriesList();
        }

        public static ForkGlobalData Instance => new ForkGlobalData();
        public ForkGlobalData() 
        { 
            
        }


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

        #endregion

        #region Value Checks

        public static bool CheckIfValueIsInteger(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        #endregion

        

        public void ListItemSelected(object obj)
        {
            
        }

        public void ListItemSelected(object obj, ListItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        

       
    }
}
