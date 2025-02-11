using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Awake()
    {
        // Ensure there's only one InventoryManager
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Add an item to the inventory
    public void AddItem(string itemName, int amount = 1)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += amount;
        }
        else
        {
            inventory[itemName] = amount;
        }

        Debug.Log(itemName + " added. Total: " + inventory[itemName]);
    }

    // Remove an item from the inventory
    public bool RemoveItem(string itemName, int amount = 1)
    {
        if (inventory.ContainsKey(itemName) && inventory[itemName] >= amount)
        {
            inventory[itemName] -= amount;

            if (inventory[itemName] <= 0)
                inventory.Remove(itemName);

            Debug.Log(itemName + " removed. Remaining: " + (inventory.ContainsKey(itemName) ? inventory[itemName] : 0));
            return true;
        }

        Debug.Log("Not enough " + itemName);
        return false;
    }

    // Get the quantity of an item
    public int GetItemCount(string itemName)
    {
        return inventory.ContainsKey(itemName) ? inventory[itemName] : 0;
    }

    // Check if an item exists in the inventory
    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName);
    }
}
