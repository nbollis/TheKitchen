using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public abstract class Item
    {
        protected readonly string Name;
        protected List<string> Unit; 
        protected List<double> Quantity;

        public Item(string name)
        {
            Name = name;
            this.InitializeLists();
        }

        public static void FindCheapest(int items = 1)
        {

        }



        public override string ToString()
        {
            string output = "Item:{0}:{1}:", Name, ExpirationDate;
            for (int i = 0; i < Unit.Count; i++)
            {
                if (i == Unit.Count - 1)
                {
                    output += Quantity[Unit.IndexOf(Unit[i])].ToString() + "," + Unit[i] + ":";
                }
                else
                {
                    output += Quantity[Unit.IndexOf(Unit[i])].ToString() + "," + Unit[i] + ",";
                }
            }
            return output;
        }

        private void InitializeLists()
        {
            Quantity = new List<double>();
            Unit = new List<string>();
        }
    }
}
