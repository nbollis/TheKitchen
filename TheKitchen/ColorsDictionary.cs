using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public static class ColorsDictionary
    {
        public static Dictionary<string, string> Colors { get; set; }

        static ColorsDictionary()
        {
            InitializeDictionary();
        }


        public static void InitializeDictionary()
        {
            Colors = new Dictionary<string, string>();
            Colors.Add("Thistle", "#e0bbe4");
            Colors.Add("LavenderPurple", "#957dad");
            Colors.Add("PastelViolet", "#d291bc");
            Colors.Add("CottonCandy", "#fec8d8");
            Colors.Add("Lumber", "#ffdfd3");
        }

        public static string ConvertToXamlMarkup()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var color in Colors)
            {
                sb.Append($"<Color x:Key=\"{color.Key}\"> {color.Value} </Color>\n");
                sb.Append($"<SolidColorBrush x:Key=\"{color.Key}Brush\" Color=\"{{StaticResource {color.Key}}}\" /> \n\n");
            }

            return sb.ToString();
        }
    }
}
