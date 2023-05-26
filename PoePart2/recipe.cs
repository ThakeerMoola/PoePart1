﻿

using System;
using System.Collections.Generic;
using System.Linq;

namespace PoePart2
{
    internal class Recipe
    {
        // Define a delegate for the calorie notification
        public delegate void CalorieNotification(int totalCalories);

        // Define an event based on the delegate
        public event CalorieNotification OnCalorieExceeded;

        public string RecipeName { get; set; }
        private List<string> ingredients;
        private List<string> units;
        private List<double> quantity;
        private List<int> calories;
        private List<string> foodgroup;
        private string[] steps;

        private static List<string> availableFoodGroups = new List<string>
        {
            "Starchy foods",
            "Vegetables and fruits",
            "Dry beans, peas, lentils and soya",
            "Chicken, fish, meat and eggs",
            "Milk and dairy products",
            "Fats and oil",
            "Water"
        };

        // Constructor
        public Recipe()
        {
            ingredients = new List<string>();
            units = new List<string>();
            quantity = new List<double>();
            calories = new List<int>();
            foodgroup = new List<string>();

        }

        // Display recipe instructions
        public void DisplayRecipe()
        {
            int totalCalories = calories.Sum();

            Console.WriteLine("--------------------------------\n" +
                $"Recipe Ingredients for {RecipeName}:\n" +
                "--------------------------------\n");

            for (int i = 0; i < ingredients.Count; i++)
            {
                Console.WriteLine($"\n{ingredients[i]} - {quantity[i]} {units[i]}\n {calories[i]} Cals \n Food groups: {foodgroup[i]}");
            }

            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            if (totalCalories < 100)
            {
                Console.WriteLine("Low calorie encounter, this recipe is perfect for people on a diet");
            }
            else if (totalCalories > 100 && totalCalories <= 300)
            {
                Console.WriteLine("Moderate calorie encounter, this recipe is suitable for most people");
            }
            else
            {
                // If the total calories exceed 300, raise the event to notify the user
                OnCalorieExceeded?.Invoke(totalCalories);
            }
        }

        // Add a recipe
        public void AddRecipe(List<Recipe> recipes)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            try
            {
                Console.WriteLine("Please enter the name of the recipe");
                RecipeName = Console.ReadLine();

                Console.WriteLine("\nPlease enter the number of ingredients for your special dish:");
                int amount = int.Parse(Console.ReadLine());

                for (int i = 0; i < amount; i++)
                {
                    Console.WriteLine($"\nEnter ingredient {i + 1} for {RecipeName}:");
                    ingredients.Add(Console.ReadLine());

                    Console.WriteLine($"Enter calories for {ingredients[i]} for {RecipeName}:");
                    calories.Add(int.Parse(Console.ReadLine()));
                
            
                    Console.WriteLine($"Select the food group from the list:\n");
                    DisplayAvailableFoodGroups();
                    Console.WriteLine($"Enter the food group index for {ingredients[i]} for {RecipeName}:");
                
                    int foodGroupIndex;
                    if (int.TryParse(Console.ReadLine(), out foodGroupIndex) && foodGroupIndex >= 0 && foodGroupIndex < availableFoodGroups.Count)
                    {
                        foodgroup.Add(availableFoodGroups[foodGroupIndex]);
                    }
                    else
                    {
                        foodgroup.Add("Unknown");
                    }

                    Console.WriteLine($"Enter quantity for {ingredients[i]} for {RecipeName}:");
                    quantity.Add(double.Parse(Console.ReadLine()));

                    Console.WriteLine($"Enter unit for {ingredients[i]} for {RecipeName}:");
                    units.Add(Console.ReadLine());
                }

                Console.WriteLine("\nPlease enter the number of steps involved in this recipe:");
                int numsteps = int.Parse(Console.ReadLine());

                steps = new string[numsteps];

                for (int i = 0; i < numsteps; i++)
                {
                    Console.WriteLine($"\nEnter step {i + 1} and a description:");
                    steps[i] = Console.ReadLine();
                }


                Console.WriteLine($"\n{RecipeName} recipe has been added successfully!\n");

                recipes.Add(this);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.ResetColor();
        }

        // Clear all recipes
        public static void ClearRecipes(List<Recipe> recipes)
        {
            recipes.Clear();
            Console.WriteLine("\nAll recipes have been cleared.");
        }

        // Display available food groups
        private void DisplayAvailableFoodGroups()
        {
            for (int i = 0; i < availableFoodGroups.Count; i++)
            {
                Console.WriteLine($"{i}: {availableFoodGroups[i]}");
            }
        }
    }
}

