using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public interface IRecipe
    {
        public string Name { get; set; }
        public int Serves { get; set; }
        public List<string> Procedure { get; set; }
        public List<string> Notes { get; set; }
        public List<string> Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<CookInstance> CookInstances { get; set; }
        public string Description { get; set; }
        public bool Changed { get; set; }
        public string ImageFilePath { get; set; }
    }
}
