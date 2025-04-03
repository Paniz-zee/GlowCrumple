using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    public TMP_Text bowlText;
    public TMP_Text availabilityText;

    private string currentIngredients = "";
    private Dictionary<string, int> ingredientClickCount = new Dictionary<string, int>();

    // List of allowed ingredients
    private HashSet<string> validIngredients = new HashSet<string> { "egg", "butter", "flour" };

    public void AddIngredient(string ingredientName)
    {
        availabilityText.gameObject.SetActive(false);

        // Check if the ingredient is valid
        if (!validIngredients.Contains(ingredientName))
        {
            bowlText.text = "Incorrect ingredient!";
            return;
        }

        // Track ingredient clicks
        if (ingredientClickCount.ContainsKey(ingredientName))
        {
            ingredientClickCount[ingredientName]++;
        }
        else
        {
            ingredientClickCount[ingredientName] = 1;
        }

        // If ingredient is clicked more than 3 times, remove it
        if (ingredientClickCount[ingredientName] > 3)
        {
            RemoveIngredient(ingredientName);
        }
        else
        {
            // Add ingredient to the bowl
            if (string.IsNullOrEmpty(currentIngredients))
            {
                currentIngredients = ingredientName;
            }
            else
            {
                currentIngredients += ", " + ingredientName;
            }

            UpdateBowl();
        }
    }

    private void RemoveIngredient(string ingredientName)
    {
        currentIngredients = currentIngredients.Replace(ingredientName, "").Trim();
        UpdateBowl();
        bowlText.text = "You are out of " + ingredientName + "!";

        // Destroy the ingredient GameObject if it exists
        GameObject ingredientObject = GameObject.Find(ingredientName);
        if (ingredientObject != null)
        {
            Destroy(ingredientObject);
        }
    }

    private void UpdateBowl()
    {
        bowlText.text = "Added: " + currentIngredients;
    }

}
