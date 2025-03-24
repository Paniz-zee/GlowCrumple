


using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    // Reference to the Text component displaying the ingredients in the bowl
    public TMP_Text bowlText; // Assuming you're using TMP_Text to display the ingredients in the bowl

    // List to keep track of ingredients added to the bowl
    private string currentIngredients = "";

    // Dictionary to track the number of times an ingredient has been clicked
    private Dictionary<string, int> ingredientClickCount = new Dictionary<string, int>();

    // This method will be called when an ingredient is clicked
    public void AddIngredient(string ingredientName)
    {
        // Check if the ingredient has been clicked more than 3 times
        if (ingredientClickCount.ContainsKey(ingredientName))
        {
            ingredientClickCount[ingredientName]++;
        }
        else
        {
            ingredientClickCount[ingredientName] = 1;
        }

        // If the ingredient has been clicked more than 3 times, remove it
        if (ingredientClickCount[ingredientName] > 3)
        {
            RemoveIngredient(ingredientName);
        }
        else
        {
            // Add the ingredient to the list of current ingredients in the bowl
            if (string.IsNullOrEmpty(currentIngredients))
            {
                currentIngredients = ingredientName;
            }
            else
            {
                currentIngredients += ", " + ingredientName;
            }

            // Update the bowl display
            UpdateBowl();
        }
    }

    // This method removes an ingredient from the scene and updates the message
    private void RemoveIngredient(string ingredientName)
    {
        // Remove the ingredient from the bowl's ingredient list
        currentIngredients = currentIngredients.Replace(ingredientName, "").Trim();

        // Update the bowl display
        UpdateBowl();

        // Display the message indicating the ingredient is out
        bowlText.text = "You are out of " + ingredientName + "!";

        // Find and destroy the ingredient's GameObject in the scene
        GameObject ingredientObject = GameObject.Find(ingredientName);
        if (ingredientObject != null)
        {
            Destroy(ingredientObject); // Destroy the ingredient GameObject
        }
    }

    // This method updates the text display of the bowl
    private void UpdateBowl()
    {
        bowlText.text = currentIngredients + " Added: ";
    }
}
