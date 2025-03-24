using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientManager : MonoBehaviour
{
   
    // Reference to the Text component displaying the ingredients in the bowl
    public TMP_Text bowlText; // Assuming you're using Text to display the ingredients in the bowl

    // List to keep track of ingredients added to the bowl
    private string currentIngredients = "";

    // This method will be called when an ingredient is clicked
    public void AddIngredient(string ingredientName)
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

    // This method updates the text display of the bowl
    private void UpdateBowl()
    {
        bowlText.text =   currentIngredients + " " + "Added: ";
    }
}
