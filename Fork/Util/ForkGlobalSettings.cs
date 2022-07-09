using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fork
{
    public static class ForkGlobalSettings
    {
        public static List<string> AcceptedPictureFormats = new List<string> { ".jpg", ".png", ".jpeg" };
        public static List<string> AcceptedRecipeUploadFormats = new List<string> { ".txt" };

        public static bool CheckIfValueIsInteger(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }
    }
}
