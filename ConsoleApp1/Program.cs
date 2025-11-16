using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlannerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new DataStore();
            var planner = new MealPlanner(store);

            Console.Write("Digite seu nome: ");
            var user = new User(Console.ReadLine()!);

            int opc = -1;
            while (opc != 0)
            {
                Console.WriteLine("\n===== MENU PRINCIPAL =====");
                Console.WriteLine("1 - Cadastrar Receita");
                Console.WriteLine("2 - Listar Receitas");
                Console.WriteLine("3 - Sugerir Receitas");
                Console.WriteLine("4 - Criar Menu e Lista de Compras");
                Console.WriteLine("5 - Calcular Calorias de uma Receita");
                Console.WriteLine("6 - Calcular Sustentabilidade de uma Receita");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha: ");
                opc = int.Parse(Console.ReadLine()!);

                switch (opc)
                {
                    case 1:
                        CadastrarReceita(store);
                        break;
                    case 2:
                        ListarReceitas(store);
                        break;
                    case 3:
                        SugerirReceitas(planner, user);
                        break;
                    case 4:
                        CriarMenu(store);
                        break;
                    case 5:
                        CalcularCalorias(store);
                        break;
                    case 6:
                        CalcularSustentabilidade(store);
                        break;
                }
            }

            Console.WriteLine("Encerrado.");
        }

        // =============================================
        // MÉTODOS DO MENU
        // =============================================

        static void CadastrarReceita(DataStore store)
        {
            Console.Write("\nNome da Receita: ");
            string nome = Console.ReadLine()!;
            var r = new Recipe(nome);

            Console.WriteLine("Informe tags (ex: vegetariano, light). Vazio para parar.");
            while (true)
            {
                Console.Write("Tag: ");
                string tag = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(tag)) break;
                r.Tags.Add(tag);
            }

            Console.WriteLine("Cadastrar Ingredientes:");
            while (true)
            {
                Console.Write("Nome do Ingrediente (vazio para parar): ");
                string ingNome = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(ingNome)) break;

                Console.Write("Calorias: ");
                int cal = int.Parse(Console.ReadLine()!);

                Console.Write("Impacto Ambiental (0 a 10): ");
                double imp = double.Parse(Console.ReadLine()!);

                r.AddIngredient(new Ingredient(ingNome, cal, imp));
            }

            store.Recipes.Add(r);
            Console.WriteLine("Receita cadastrada!");
        }

        static void ListarReceitas(DataStore store)
        {
            Console.WriteLine("\n=== LISTA DE RECEITAS ===");
            foreach (var r in store.Recipes)
                Console.WriteLine(r);
        }

        static void SugerirReceitas(MealPlanner planner, User user)
        {
            Console.Write("\nInforme uma preferência (ex: vegetariano): ");
            string pref = Console.ReadLine()!;
            user.AddPreference(pref);

            Console.Write("Quantas receitas sugerir? ");
            int q = int.Parse(Console.ReadLine()!);

            var sugestoes = planner.SuggestRecipes(user, q);

            Console.WriteLine("\nSugestões:");
            foreach (var r in sugestoes)
                Console.WriteLine(r);
        }

        static void CriarMenu(DataStore store)
        {
            Console.Write("\nNome do menu: ");
            string nome = Console.ReadLine()!;
            var menu = new Menu(nome);

            Console.WriteLine("Digite nomes das receitas para adicionar. Vazio para parar.");
            while (true)
            {
                Console.Write("Receita: ");
                string n = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(n)) break;

                var r = store.Recipes.FirstOrDefault(x => x.Name == n);
                if (r != null) menu.AddRecipe(r);
            }

            var gl = GroceryList.GenerateFromMenu(menu);

            Console.WriteLine("\n--- Lista de Compras ---");
            gl.PrintList();
        }

        static void CalcularCalorias(DataStore store)
        {
            var calc = new NutritionCalculator();

            Console.Write("\nReceita: ");
            string nome = Console.ReadLine()!;
            var r = store.Recipes.FirstOrDefault(x => x.Name == nome);

            if (r == null) { Console.WriteLine("Não encontrada!"); return; }

            Console.WriteLine($"Total de Calorias: {calc.CalculateCalories(r)} kcal");
        }

        static void CalcularSustentabilidade(DataStore store)
        {
            var calc = new SustainabilityCalculator();

            Console.Write("\nReceita: ");
            string nome = Console.ReadLine()!;
            var r = store.Recipes.FirstOrDefault(x => x.Name == nome);

            if (r == null) { Console.WriteLine("Não encontrada!"); return; }

            Console.WriteLine($"Sustentabilidade: {calc.CalculateScore(r):F2}");
        }
    }

    // =============================================
    // CLASSES DO SISTEMA
    // =============================================

    public class MealPlanner
    {
        private readonly DataStore _store;

        public MealPlanner(DataStore store)
        {
            _store = store;
        }

        public List<Recipe> SuggestRecipes(User user, int count)
        {
            var candidates = _store.Recipes.AsEnumerable();

            foreach (var pref in user.Preferences)
            {
                candidates = candidates.Where(r =>
                    r.Tags.Contains(pref) ||
                    r.Ingredients.Any(i => i.Name.Contains(pref))
                );
            }

            return candidates.Take(count).ToList();
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; } = new List<Ingredient>();
        public List<string> Tags { get; } = new List<string>();

        public Recipe(string name) { Name = name; }

        public void AddIngredient(Ingredient ing) => Ingredients.Add(ing);
        public int GetCalories() => Ingredients.Sum(i => i.Calories);
        public double GetSustainabilityScore() => Ingredients.Average(i => i.EnvironmentalImpactScore);

        public override string ToString()
        {
            return $"{Name} [{string.Join(", ", Tags)}] - {Ingredients.Count} ingredientes";
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public double EnvironmentalImpactScore { get; set; }

        public Ingredient(string name, int calories, double score)
        {
            Name = name;
            Calories = calories;
            EnvironmentalImpactScore = score;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public List<string> Preferences { get; } = new List<string>();

        public User(string name) { Name = name; }
        public void AddPreference(string p)
        {
            if (!Preferences.Contains(p)) Preferences.Add(p);
        }
    }

    public class Menu
    {
        public string Title { get; set; }
        public List<Recipe> Recipes { get; } = new List<Recipe>();

        public Menu(string t) { Title = t; }
        public void AddRecipe(Recipe r) => Recipes.Add(r);
    }

    public class GroceryList
    {
        private readonly Dictionary<string, int> _items = new();

        public void AddItem(string name)
        {
            if (_items.ContainsKey(name)) _items[name]++;
            else _items[name] = 1;
        }

        public static GroceryList GenerateFromMenu(Menu menu)
        {
            var g = new GroceryList();
            foreach (var r in menu.Recipes)
                foreach (var ing in r.Ingredients)
                    g.AddItem(ing.Name);
            return g;
        }

        public void PrintList()
        {
            foreach (var i in _items)
                Console.WriteLine($"{i.Key} x{i.Value}");
        }
    }

    public class NutritionCalculator
    {
        public int CalculateCalories(Recipe r) => r.GetCalories();
    }

    public class SustainabilityCalculator
    {
        public double CalculateScore(Recipe r) => r.GetSustainabilityScore();
    }

    public class DataStore
    {
        public List<Recipe> Recipes { get; } = new();

        public void Save() => Console.WriteLine("Dados salvos (simulado).");
    }

    public static class Notification
    {
        public static void SendEmail(User user, string msg)
        {
            Console.WriteLine($"Email enviado para {user.Name}: {msg}");
        }
    }
}
