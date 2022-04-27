using System;
using System.Collections.Generic;

namespace TheKitchen
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Recipe> recipes = new();
            recipes.Add(new Recipe(@"A:\Downloads\RecipesToConvert\Kolhapuri Tambda Rassa.txt"));
            recipes.Add(new Recipe(@"A:\Downloads\RecipesToConvert\Xacuti.txt"));
            recipes.Add(new Recipe(@"A:\Downloads\RecipesToConvert\Chicken Fajita Pasta.txt"));



            int breakpoint = 0;
        }
    }
}
