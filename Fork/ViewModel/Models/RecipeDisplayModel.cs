using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TheKitchen;

namespace Fork
{
    public class RecipeDisplayModel : RecipeDisplayViewModel
    {
        public static RecipeDisplayModel Instance => new RecipeDisplayModel();

        public RecipeDisplayModel() : base(XmlReaderWriter.ReadFromXmlFile<Recipe>(@"C:\Users\nboll\source\repos\TheKitchen\TheKitchen\DataFiles\Recipes\Chicken Fajita Pasta.xml"))
        {
            
        }

    }
}
