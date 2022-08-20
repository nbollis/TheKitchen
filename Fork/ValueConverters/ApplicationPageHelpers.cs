using System;
using System.Diagnostics;
using System.Globalization;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Fork
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Recipes:
                    return new RecipePageView(viewModel as RecipesPageViewModel);

                case ApplicationPage.MealPrep:
                    return new MealPrepPageView(viewModel as MealPrepPageViewModel);

                case ApplicationPage.GroceryList:
                    return new GroceryListPageView(viewModel as GroceryListPageViewModel);

                case ApplicationPage.Ingredients:
                    return new IngredientsPageView(viewModel as IngredientsPageViewModel);

                case ApplicationPage.Techniques:
                    return new TechniquesPageView(viewModel as TechniquesPageViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is RecipePageView)
                return ApplicationPage.Recipes;
            if (page is MealPrepPageView)
                return ApplicationPage.MealPrep;
            if (page is GroceryListPageView)
                return ApplicationPage.GroceryList;
            if (page is IngredientsPageView)
                return ApplicationPage.Ingredients;
            if (page is TechniquesPageView)
                return ApplicationPage.Techniques;

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
