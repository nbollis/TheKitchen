using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkDataHandling;
using NUnit.Framework;
using TheKitchen;

namespace Tests
{
    public class TestProcedureAndProcedureStep
    {

        public static Recipe Fajita { get; set; }

        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            Fajita = RecipeParser.ParseRecipes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes\xml"), out List<string> errors).First();
        }

        [Test]
        public static void TestProcedureStartTimeInitiation()
        {
            for (var i = 0; i < Fajita.Procedure.Procedures.Count; i++)
            {
                Assert.That(Fajita.Procedure.Procedures[i].StepNumber == i + 1);
            }

            Procedure procedure = Fajita.Procedure;
            Assert.That(!procedure.StopWatch.IsRunning);
            Assert.That(!procedure.Procedures.All(p => p.StopWatch.IsRunning));
            procedure.StartProcedure();
            Assert.That(procedure.StopWatch.IsRunning);
            Assert.That(procedure.Procedures.First().StopWatch.IsRunning);
            Assert.That(procedure.Procedures.Where(p => p.StepNumber != 1).All(p => !p.StopWatch.IsRunning));
        }

        [Test]
        public static void TestProcedureMoveNextStep()
        {
            Procedure procedure = Fajita.Procedure;
            procedure.StartProcedure();
            // all steps except the last one
            for (int i = 0; i < Fajita.Procedure.Procedures.Count - 1; i++)
            {
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i + 1].StopWatch.IsRunning);
                procedure.MoveToNextStep();
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i + 1].StopWatch.IsRunning);
            }

            // final step
            procedure.MoveToNextStep();
            Assert.That(!procedure.StopWatch.IsRunning);
            Assert.That(procedure.Procedures.All(p => !p.StopWatch.IsRunning));
        }

        [Test]
        public static void TestErrors()
        {
            Procedure procedure = Fajita.Procedure;
            try
            {
                ((ITimeable)procedure.Procedures[3]).Stop();
            }
            catch (ArgumentException e)
            {
                Assert.That(e.Message == "Step not started");
            }
        }

        [Test]
        public static void TestAverageTimeToPerform()
        {
            Fajita = RecipeParser.ParseRecipes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\Recipes\xml"), out List<string> errors).First();
            Procedure procedure = Fajita.Procedure;
            List<TimeSpan> lastStepElapsed = new();
            List<TimeSpan> procedureElapsed = new();

            Assert.That(procedure.Procedures.All(p => p.TimeToPerform.Count == 0));
            procedure.StartProcedure();
            for (int i = 0; i < Fajita.Procedure.Procedures.Count - 1; i++)
            {
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i + 1].StopWatch.IsRunning);
                procedure.MoveToNextStep();
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i + 1].StopWatch.IsRunning);
            }
            procedure.MoveToNextStep();
            lastStepElapsed.Add(procedure.Procedures.Last().StopWatch.Elapsed);
            procedureElapsed.Add(procedure.StopWatch.Elapsed);
            Assert.That(procedure.Procedures.All(p => p.TimeToPerform.Count == 1));


            procedure.StartProcedure();
            for (int i = 0; i < Fajita.Procedure.Procedures.Count - 1; i++)
            {
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i + 1].StopWatch.IsRunning);
                procedure.MoveToNextStep();
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i + 1].StopWatch.IsRunning);
            }
            procedure.MoveToNextStep();
            lastStepElapsed.Add(procedure.Procedures.Last().StopWatch.Elapsed);
            procedureElapsed.Add(procedure.StopWatch.Elapsed);
            Assert.That(procedure.Procedures.All(p => p.TimeToPerform.Count == 2));

            procedure.StartProcedure();
            for (int i = 0; i < Fajita.Procedure.Procedures.Count - 1; i++)
            {
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i + 1].StopWatch.IsRunning);
                procedure.MoveToNextStep();
                Assert.That(procedure.StopWatch.IsRunning);
                Assert.That(!procedure.Procedures[i].StopWatch.IsRunning);
                Assert.That(procedure.Procedures[i + 1].StopWatch.IsRunning);
            }
            procedure.MoveToNextStep();
            lastStepElapsed.Add(procedure.Procedures.Last().StopWatch.Elapsed);
            procedureElapsed.Add(procedure.StopWatch.Elapsed);
            Assert.That(procedure.Procedures.All(p => p.TimeToPerform.Count == 3));

            // test averaging on final step
            for (var i = 0; i < lastStepElapsed.Count; i++)
            {
                var step = lastStepElapsed[i];
                Assert.That(procedure.Procedures.Last().TimeToPerform[i].Ticks == step.Ticks);
            }
            var lastStepAverage = TimeSpan.FromMilliseconds(lastStepElapsed.Select(p => p.TotalMilliseconds)
                .Average());
            Assert.That(lastStepAverage.TotalMilliseconds == procedure.Procedures.Last().AverageTimeToPerform.TotalMilliseconds);

            // test averaging on whole procedure
            for (var i = 0; i < procedureElapsed.Count; i++)
            {
                var step = procedureElapsed[i];
                Assert.That(procedure.TimeToPerform[i].Ticks == step.Ticks);
            }
            var procedureAverage = TimeSpan.FromMilliseconds(procedureElapsed.Select(p => p.TotalMilliseconds).Average());
            Assert.That(procedureAverage.TotalMilliseconds == procedure.AverageTimeToPerform.TotalMilliseconds);
        }

        //[Test]
        //public static void TempForRestructuringRecipe()
        //{
        //    string recipeDirectory = @"C:\Users\nboll\source\repos\TheKitchen\TheKitchen\DataFiles\Recipes";
        //    string[] recipeFiles = Directory.GetFiles(recipeDirectory);
        //    List<Recipe> recipes = new List<Recipe>();
        //    foreach (string file in recipeFiles)
        //    {
        //        var oldRecipe = XmlReaderWriter.ReadFromXmlFile<OldRecipe>(file);
        //        File.Delete(file);
        //        var newRecipe = new Recipe(oldRecipe);
        //        string path = Path.Combine(recipeDirectory, newRecipe.Name + ".xml");
        //        XmlReaderWriter.WriteToXmlFile(path, newRecipe);
        //    }

            

        //}
    }
}
