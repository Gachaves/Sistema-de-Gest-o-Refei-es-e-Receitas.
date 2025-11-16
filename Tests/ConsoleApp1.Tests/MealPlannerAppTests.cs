using System;
using System.Collections.Generic;
using System.Linq;
using MealPlannerApp;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class MealPlannerAppTests
    {
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
            var r = new Recipe("Salada");
            r.AddIngredient(new Ingredient("Tomate", 20, 2));
            r.AddIngredient(new Ingredient("Alface", 5, 4));

            Assert.Equal(3, r.GetSustainabilityScore());
        }

        [Fact]
        public void Ingredient_Should_Store_Values_Correctly()
        {
            var ing = new Ingredient("Arroz", 150, 3.5);
            Assert.Equal("Arroz", ing.Name);
            Assert.Equal(150, ing.Calories);
            Assert.Equal(3.5, ing.EnvironmentalImpactScore);
        }

        [Fact]
        public void MealPlanner_Should_Suggest_Only_Matching_Tags()
        {
            var store = new DataStore();

            var r1 = new Recipe("Salada Veg");
            r1.Tags.Add("vegetariano");

            var r2 = new Recipe("Carne Assada");
            r2.Tags.Add("carnes");

            store.Recipes.Add(r1);
            store.Recipes.Add(r2);

            var planner = new MealPlanner(store);
            var user = new User("Gabriel");
            user.AddPreference("vegetariano");

            var result = planner.SuggestRecipes(user, 10);

            Assert.Single(result);
            Assert.Equal("Salada Veg", result[0].Name);
        }

        [Fact]
        public void MealPlanner_Should_Return_All_Recipes_When_No_Preferences()
        {
            var store = new DataStore();
            var r1 = new Recipe("A");
            var r2 = new Recipe("B");

            store.Recipes.Add(r1);
            store.Recipes.Add(r2);

            var planner = new MealPlanner(store);
            var user = new User("Gabriel");

            var result = planner.SuggestRecipes(user, 10);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GroceryList_Should_Count_Ingredients()
        {
            var r = new Recipe("Macarrão");
            r.AddIngredient(new Ingredient("Macarrão", 200, 3));
            r.AddIngredient(new Ingredient("Molho", 50, 2));

            var menu = new Menu("Jantar");
            menu.AddRecipe(r);
            menu.AddRecipe(r);

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
            var menu = new Menu("Vazio");
            var gl = GroceryList.GenerateFromMenu(menu);

            var field = typeof(GroceryList).GetField("_items",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            var dict = (Dictionary<string, int>)field!.GetValue(gl)!;
            Assert.Empty(dict);
        }

        [Fact]
        public void Notification_Should_Send_Email()
        {
            var user = new User("Gabriel");
            var exception = Record.Exception(() => Notification.SendEmail(user, "Olá!"));
            Assert.Null(exception);
        }

        [Fact]
        public void User_Should_Add_Preference_Once()
        {
            var u = new User("Gabriel");
            u.AddPreference("vegano");
            u.AddPreference("vegano");

            Assert.Single(u.Preferences);
        }

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

        [Fact]
        public void Menu_Should_Have_Title_And_AddRecipes()
        {
            var menu = new Menu("Menu X");
            var r = new Recipe("Sopa");
            menu.AddRecipe(r);

            Assert.Equal("Menu X", menu.Title);
            Assert.Contains(r, menu.Recipes);
        }
    }
}
