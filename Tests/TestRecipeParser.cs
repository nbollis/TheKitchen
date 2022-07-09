using NUnit.Framework;
using TheKitchen;
using System.Collections.Generic;
using System.IO;
using System;
using ForkIO;

namespace Tests
{
    public class TestRecipeParser
    {
        [SetUp]
        public void Setup()
        {
        }

        // NOTE: Something is causing every recipe to be saved as an xml to test/bin/debug/net6.0/dagafiles/recipes 
        // no clue what it is but its not my vibe to work on that atm
        [Test]
        public void LoadSaveRecipe()
        {
            List<Recipe> xmlRecipes = RecipeParser.ParseRecipes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes\xml"), out List<string> errors);
            Assert.That(errors.Count == 0);
            Assert.That(xmlRecipes.Count, Is.EqualTo(3));
            Recipe chixFajita = xmlRecipes[0];
            Recipe couscous = xmlRecipes[1];
            Recipe xacuti = xmlRecipes[2];

            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes", xacuti.Name + ".xml"));
            xacuti.SaveRecipe();
            Assert.That(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes", xacuti.Name + ".xml")));
        }

        [Test]
        public void LoadDataFromXml()
        {
            List<Recipe> xmlRecipes = RecipeParser.ParseRecipes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes\xml"), out List<string> errors);
            Assert.That(errors.Count == 0); Assert.That(xmlRecipes.Count, Is.EqualTo(3));
            Recipe chixFajita = xmlRecipes[0];
            Recipe couscous = xmlRecipes[1];
            Recipe xacuti = xmlRecipes[2];

            Assert.That(chixFajita.Name, Is.EqualTo("Chicken Fajita Pasta"));
            Assert.That(chixFajita.Serves, Is.EqualTo(4));
            Assert.That(chixFajita.Ingredients.Count, Is.EqualTo(14));
            Assert.That(chixFajita.Ingredients[0].Name, Is.EqualTo("oil"));
            Assert.That(chixFajita.Procedure.Count, Is.EqualTo(7));
            Assert.That(chixFajita.CookInstances.Count, Is.EqualTo(1));
            Assert.That(chixFajita.Description, Is.EqualTo(""));

            Assert.That(xacuti.Name, Is.EqualTo("Xacuti"));
            Assert.That(xacuti.Serves, Is.EqualTo(4));
            Assert.That(xacuti.Ingredients.Count, Is.EqualTo(16));
            Assert.That(xacuti.Ingredients[0].Name, Is.EqualTo("vegetable oil"));
            Assert.That(xacuti.Procedure.Count, Is.EqualTo(3));
            Assert.That(xacuti.CookInstances.Count, Is.EqualTo(0));
            Assert.That(xacuti.Description, Is.EqualTo(
                "Pronounced 'shakooti', this Portuguese-inspired curry is also called 'sagoti', " +
                "and can be made with prawns or meat. It is a thick curry made as hot or mild as " +
                "the cook likes it, and is a part of the local Christian cuisine all over Goa"));

            Assert.That(couscous.Name, Is.EqualTo("Roasted Vegetable Couscous Salad"));
            Assert.That(couscous.Serves, Is.EqualTo(4));
            Assert.That(couscous.Ingredients.Count, Is.EqualTo(13));
            Assert.That(couscous.Ingredients[0].Name, Is.EqualTo("Potatoes"));
            Assert.That(couscous.Procedure.Count, Is.EqualTo(5));
            Assert.That(couscous.CookInstances.Count, Is.EqualTo(1));
            Assert.That(couscous.Description, Is.EqualTo(""));
        }

        [Test]
        public void LoadDataFromOneNote()
        {
            List<Recipe> oneRecipes = RecipeParser.ParseRecipes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes\one"), out List<string> errors);
            Assert.That(errors.Count == 0); Assert.That(oneRecipes.Count, Is.EqualTo(3));
            Recipe chixFajita = oneRecipes[0];
            Recipe couscous = oneRecipes[1];
            Recipe xacuti = oneRecipes[2];

            Assert.That(chixFajita.Name, Is.EqualTo("Chicken Fajita Pasta"));
            Assert.That(chixFajita.Serves, Is.EqualTo(4));
            Assert.That(chixFajita.Ingredients.Count, Is.EqualTo(14));
            Assert.That(chixFajita.Ingredients[0].Name, Is.EqualTo("oil"));
            Assert.That(chixFajita.Procedure.Count, Is.EqualTo(7));
            Assert.That(chixFajita.CookInstances.Count, Is.EqualTo(1));
            Assert.That(chixFajita.Description, Is.EqualTo(""));

            Assert.That(xacuti.Name, Is.EqualTo("Xacuti"));
            Assert.That(xacuti.Serves, Is.EqualTo(4));
            Assert.That(xacuti.Ingredients.Count, Is.EqualTo(16));
            Assert.That(xacuti.Ingredients[0].Name, Is.EqualTo("vegetable oil"));
            Assert.That(xacuti.Procedure.Count, Is.EqualTo(3));
            Assert.That(xacuti.CookInstances.Count, Is.EqualTo(0));
            Assert.That(xacuti.Description, Is.EqualTo(
                "Pronounced 'shakooti', this Portuguese-inspired curry is also called 'sagoti', " +
                "and can be made with prawns or meat. It is a thick curry made as hot or mild as " +
                "the cook likes it, and is a part of the local Christian cuisine all over Goa"));

            Assert.That(couscous.Name, Is.EqualTo("Roasted Vegetable Couscous Salad"));
            Assert.That(couscous.Serves, Is.EqualTo(4));
            Assert.That(couscous.Ingredients.Count, Is.EqualTo(13));
            Assert.That(couscous.Ingredients[0].Name, Is.EqualTo("Potatoes"));
            Assert.That(couscous.Procedure.Count, Is.EqualTo(5));
            Assert.That(couscous.CookInstances.Count, Is.EqualTo(1));
            Assert.That(couscous.Description, Is.EqualTo(""));
        }


    }
}