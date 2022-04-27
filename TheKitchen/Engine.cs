using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class Engine
    {
        private List<Item> Items;

        public void PrintItems()
        {
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataFiles/Items.txt");
            using (StreamWriter output = new StreamWriter(filepath))
            {
                foreach (var item in Items)
                {
                    output.WriteLine(item.ToString());
                }
            }
        }

        public void ReadItems()
        {
            string filepath = "";
            string[] lines = System.IO.File.ReadAllLines(filepath);
        }
    }
}
