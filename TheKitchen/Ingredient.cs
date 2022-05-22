using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace TheKitchen
{
    public class Ingredient 
    {
        public string Name;
        public Enum Unit;
        public string UnitString;
        public double Amount;
        public string How;
        public string Category;
        public string WholeLine;

        public Ingredient()
        {

        }

        public Ingredient(string name, double amount, string how, string unit, string ingredientsline = null)
        {
            Name = name;
            Amount = amount;
            How = how;
            UnitString = unit;
            WholeLine = ingredientsline;
        }

        public static Ingredient ParseIngredientsFromText(string ingredientLine)
        {
            if (ingredientLine.Length < 5)
                return null;

            string how = "";
            int amount = 0;
            string name = "";
            string units = "";
            Enum unit = null;

            string[] commaDeliminator = ingredientLine.Split(',');
            if (commaDeliminator.Length > 1)
            {
                how = commaDeliminator[commaDeliminator.Length - 1];
            }

            string[] inFrontOfComma = commaDeliminator[0].Split(' ');
            try
            {
                amount = Int32.Parse(inFrontOfComma[0].Trim());
            }
            catch (Exception e) { }

            if (!inFrontOfComma[1].Trim().Equals("blank"))
            {
                units = inFrontOfComma[1].Trim(); 
            }

            // obsolete until the Unit.Net is implemented
            //bool found = UnitParser.Default.TryParse(inFrontOfComma[1].Trim(), typeof(Volume), out Enum unit1);
            //if (found)
            //{
            //    unit = unit1;
            //}
            //found = UnitParser.Default.TryParse(inFrontOfComma[1].Trim(), typeof(Mass), out Enum unit2);
            //if (found)
            //{
            //    unit = unit2;


            for (int i = 2; i<inFrontOfComma.Length; i++)
            {
                name += inFrontOfComma[i].Replace(",","").Trim() + " ";
            }
            name = name.Trim();

            return new Ingredient(name, amount, how, units, ingredientLine);
        }

        public override string ToString()
        {
            string output = "";
            if (UnitString.Equals(""))
            {
                if (How.Equals(""))
                    output += Amount + " " + Name;
                else
                    output += Amount + " " + Name + ", " + How;
            }
            else
            {
                if (How.Equals(""))
                    output += Amount + " " + UnitString + " " + Name;
                else
                    output += Amount + " " + UnitString + " " + Name + ", " + How;
            }
            return output;
        }
    }
}
