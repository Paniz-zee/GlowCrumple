using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<string> LearnedRecipes { get; private set; } = new List<string>();
    public List<string> Inventory { get; private set; } = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory(string item)
    {
        if (!Inventory.Contains(item))
        {
            Inventory.Add(item);
            Debug.Log($"Added {item} to inventory.");
        }
    }

    public void RemoveItemFromInventory(string item)
    {
        if (Inventory.Contains(item))
        {
            Inventory.Remove(item);
            Debug.Log($"Removed {item} from inventory.");
        }
    }

    public bool HasItem(string item)
    {
        return Inventory.Contains(item);
    }

    public void LearnRecipe(string recipe)
    {
        if (!LearnedRecipes.Contains(recipe))
        {
            LearnedRecipes.Add(recipe);
            Debug.Log($"Learned new recipe: {recipe}");
        }
    }

    public bool IsRecipeLearned(string recipe)
    {
        return LearnedRecipes.Contains(recipe);
    }
}
