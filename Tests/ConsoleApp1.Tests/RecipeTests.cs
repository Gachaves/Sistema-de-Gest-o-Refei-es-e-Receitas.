using Xunit;
using MealPlannerApp;

namespace ConsoleApp1.Tests
{
    public class RecipeTests
    {
        [Fact]
        public void Recipe_Should_Start_Empty()
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

            r.AddIngredient(new Ingredient("Tomate", 10, 3));

            Assert.Single(r.Ingredients);
        }

        [Fact]
        public void Recipe_Should_Calc_Calories()
        {
            var r = new Recipe("Mistura");
            r.AddIngredient(new Ingredient("A", 10, 1));
            r.AddIngredient(new Ingredient("B", 20, 1));

            Assert.Equal(30, r.GetCalories());
        }

        [Fact]
        public void Recipe_Should_Calc_Sustainability()
        {
            var r = new Recipe("Eco");
            r.AddIngredient(new Ingredient("A", 10, 2));
            r.AddIngredient(new Ingredient("B", 20, 4));

            Assert.Equal(3, r.GetSustainabilityScore());
        }
    }
}
