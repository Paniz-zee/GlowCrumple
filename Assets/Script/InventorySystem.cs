using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public List<string> ingredients = new List<string>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddIngredient(string ingredient)
    {
        ingredients.Add(ingredient);
        Debug.Log(ingredient + " added to inventory.");
    }

    public void RemoveIngredient(string ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
            Debug.Log(ingredient + " removed from inventory.");
        }
    }
}
