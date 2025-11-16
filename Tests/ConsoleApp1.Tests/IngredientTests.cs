using Xunit;
using MealPlannerApp;

namespace ConsoleApp1.Tests
{
    public class IngredientTests
    {
        [Fact]
        public void Ingredient_Should_Store_Values()
        {
            var ing = new Ingredient("Tomate", 20, 3.5);

            Assert.Equal("Tomate", ing.Name);
            Assert.Equal(20, ing.Calories);
            Assert.Equal(3.5, ing.EnvironmentalImpactScore);
        }

        [Fact]
        public void Ingredient_Should_Allow_Changing_Values()
        {
            var ing = new Ingredient("Cenoura", 30, 2.0);

            ing.Name = "Cenoura Orgânica";
            ing.Calories = 40;
            ing.EnvironmentalImpactScore = 1.5;

            Assert.Equal("Cenoura Orgânica", ing.Name);
            Assert.Equal(40, ing.Calories);
            Assert.Equal(1.5, ing.EnvironmentalImpactScore);
        }
    }
}
