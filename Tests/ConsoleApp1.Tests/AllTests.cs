using System;
using System.Collections.Generic;
using System.Linq;
using MealPlannerApp;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class AllTests
    {
        // ============================================================
        // RECIPE TESTS
        // ============================================================

        [Fact]
        public void Recipe_Should_Initialize_Empty()
        {
            var r = new Recipe("Teste");

            Assert.Equal("Teste", r.Name);
            Assert.Empty(r.Ingredients);
            Assert.Empty(r.Tags);
        }

        [Fact]
        public void Recipe_Should_Add_Ingredient()
        {
            var r = new Recipe("Salada");
            var ing = new Ingredient("Tomate", 10, 2);

            r.AddIngredient(ing);

            Assert.Single(r.Ingredients);
            Assert.Equal("Tomate", r.Ingredients[0].Name);
        }

        [Fact]
        public void Recipe_Should_Calculate_Calories()
        {
            var r = new Recipe("Salada");
            r.AddIngredient(new Ingredient("Tomate", 20, 2));
            r.AddIngredient(new Ingredient("Alface", 5, 1));

            Assert.Equal(25, r.GetCalories());
        }

        [Fact]
        public void Recipe_Should_Calculate_Sustainability()
        {
            var r = new Recipe("Teste");
            r.AddIngredient(new Ingredient("Arroz", 10, 3));
            r.AddIngredient(new Ingredient("Feijão", 20, 5));

            Assert.Equal(4, r.GetSustainabilityScore());
        }

        // ============================================================
        // INGREDIENT TESTS
        // ============================================================

        [Fact]
        public void Ingredient_Should_Store_Values()
        {
            var ing = new Ingredient("Arroz", 150, 3.5);

            Assert.Equal("Arroz", ing.Name);
            Assert.Equal(150, ing.Calories);
            Assert.Equal(3.5, ing.EnvironmentalImpactScore);
        }

        // ============================================================
        // USER TESTS
        // ============================================================

        [Fact]
        public void User_Should_Add_Preference_Once()
        {
            var u = new User("Gabriel");

            u.AddPreference("vegano");
            u.AddPreference("vegano");

            Assert.Single(u.Preferences);
        }

        [Fact]
        public void User_Should_Store_Name()
        {
            var u = new User("João");
            Assert.Equal("João", u.Name);
        }

        // ============================================================
        // MEAL PLANNER TESTS
        // ============================================================

        [Fact]
        public void MealPlanner_Should_Suggest_Only_Matching_Tag()
        {
            var store = new DataStore();

            var r1 = new Recipe("Vegano");
            r1.Tags.Add("vegano");

            var r2 = new Recipe("Carne");
            r2.Tags.Add("carne");

            store.Recipes.Add(r1);
            store.Recipes.Add(r2);

            var planner = new MealPlanner(store);
            var user = new User("Lucas");
            user.AddPreference("vegano");

            var result = planner.SuggestRecipes(user, 10);

            Assert.Single(result);
            Assert.Equal("Vegano", result[0].Name);
        }

        [Fact]
        public void MealPlanner_Should_Return_All_When_No_Preferences()
        {
            var store = new DataStore();
            store.Recipes.Add(new Recipe("A"));
            store.Recipes.Add(new Recipe("B"));

            var planner = new MealPlanner(store);
            var user = new User("Maria");

            var result = planner.SuggestRecipes(user, 10);

            Assert.Equal(2, result.Count);
        }

        // ============================================================
        // MENU TESTS
        // ============================================================

        [Fact]
        public void Menu_Should_Add_Recipes()
        {
            var menu = new Menu("Almoço");
            var r = new Recipe("Sopa");

            menu.AddRecipe(r);

            Assert.Equal("Almoço", menu.Title);
            Assert.Contains(r, menu.Recipes);
        }

        // ============================================================
        // GROCERY LIST TESTS
        // ============================================================

        [Fact]
        public void GroceryList_Should_Count_Ingredients()
        {
            var r = new Recipe("Macarrão");
            r.AddIngredient(new Ingredient("Macarrão", 200, 3));
            r.AddIngredient(new Ingredient("Molho", 50, 2));

            var menu = new Menu("Jantar");
            menu.AddRecipe(r);
            menu.AddRecipe(r); // adicionado duas vezes

            var gl = GroceryList.GenerateFromMenu(menu);

            var field = typeof(GroceryList).GetField("_items",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            var dict = (Dictionary<string, int>)field!.GetValue(gl)!;

            Assert.Equal(2, dict["Macarrão"]);
            Assert.Equal(2, dict["Molho"]);
        }

        [Fact]
        public void GroceryList_Should_Handle_Empty_Menu()
        {
            var gl = GroceryList.GenerateFromMenu(new Menu("Vazio"));

            var field = typeof(GroceryList).GetField("_items",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            var dict = (Dictionary<string, int>)field!.GetValue(gl)!;

            Assert.Empty(dict);
        }

        // ============================================================
        // CALCULATORS TESTS
        // ============================================================

        [Fact]
        public void NutritionCalculator_Should_Calc_Correctly()
        {
            var r = new Recipe("Teste");
            r.AddIngredient(new Ingredient("A", 10, 1));
            r.AddIngredient(new Ingredient("B", 20, 1));

            var calc = new NutritionCalculator();

            Assert.Equal(30, calc.CalculateCalories(r));
        }

        [Fact]
        public void SustainabilityCalculator_Should_Calc_Correctly()
        {
            var r = new Recipe("Teste");
            r.AddIngredient(new Ingredient("A", 10, 3));
            r.AddIngredient(new Ingredient("B", 20, 5));

            var calc = new SustainabilityCalculator();

            Assert.Equal(4, calc.CalculateScore(r));
        }

        // ============================================================
        // DATASTORE TESTS
        // ============================================================

        [Fact]
        public void DataStore_Should_Add_Recipes()
        {
            var store = new DataStore();
            var r = new Recipe("Pizza");

            store.Recipes.Add(r);

            Assert.Contains(r, store.Recipes);
        }

        // ============================================================
        // NOTIFICATION TEST
        // ============================================================

        [Fact]
        public void Notification_Should_Not_Throw()
        {
            var user = new User("Carlos");

            var ex = Record.Exception(() => Notification.SendEmail(user, "Olá!"));

            Assert.Null(ex);
        }
    }
}
