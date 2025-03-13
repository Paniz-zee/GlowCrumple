using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingStation : MonoBehaviour
{
    public GameObject cookingSlotPrefab;
    public Transform cookingGrid;
    public GameObject recipeButtonPrefab;
    public Transform recipeGrid;

    private List<string> currentIngredients = new List<string>();
    private List<Recipe> learnedRecipes = new List<Recipe>();

    public void AddIngredient(string itemName)
    {
        currentIngredients.Add(itemName);
        UpdateCookingUI();
    }

    void UpdateCookingUI()
    {
        foreach (Transform child in cookingGrid)
            Destroy(child.gameObject);

        foreach (string ingredient in currentIngredients)
        {
            GameObject slot = Instantiate(cookingSlotPrefab, cookingGrid);
            slot.GetComponentInChildren<Text>().text = ingredient;
        }
    }

    public void LearnRecipe(Recipe recipe)
    {
        if (!learnedRecipes.Contains(recipe))
        {
            learnedRecipes.Add(recipe);
            GameObject button = Instantiate(recipeButtonPrefab, recipeGrid);
            button.GetComponentInChildren<Text>().text = recipe.recipeName;
            button.GetComponent<Button>().onClick.AddListener(() => DisplayRecipeSlots(recipe));
        }
    }

    void DisplayRecipeSlots(Recipe recipe)
    {
        // Clear current ingredients
        currentIngredients.Clear();
        // Generate the necessary slots for the recipe
        GenerateCookingSlots(recipe.requiredIngredients.Count);

        // Optional: You can also show the required ingredients in each slot if desired
    }

    public void GenerateCookingSlots(int numberOfSlots)
    {
        foreach (Transform child in cookingGrid)
            Destroy(child.gameObject);

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = Instantiate(cookingSlotPrefab, cookingGrid);
            // Optional: Set slot's properties here (empty or showing a placeholder)
        }
    }



    void Cook(Recipe recipe)
    {
        if (recipe.CanCraft(currentIngredients))
        {
            Debug.Log($"Crafted: {recipe.recipeName}");
            currentIngredients.Clear();
            UpdateCookingUI();
        }
        else
        {
            Debug.Log("Missing ingredients!");
        }
    }
}
