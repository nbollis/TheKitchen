using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class ShoppingList
    {
        public List<IItem> Items { get; set; }

        public ShoppingList()
        {
            Items = new();
        }

        public ShoppingList(List<Recipe> recipes)
        {
            Items = new();
            foreach (var recipe in recipes)
            {
                Items.AddRange(recipe.Ingredients);
            }

            DisambiguateRepeatItems();
        }


        /// <summary>
        /// Combines items of the same type to compact the list
        /// </summary>
        private void DisambiguateRepeatItems()
        {
            // TODO: Implement this
        }
    }
}
