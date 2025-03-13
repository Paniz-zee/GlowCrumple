using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<string> requiredIngredients;

    public bool CanCraft(List<string> availableIngredients)
    {
        foreach (string ingredient in requiredIngredients)
            if (!availableIngredients.Contains(ingredient))
                return false;
        return true;
    }
}
