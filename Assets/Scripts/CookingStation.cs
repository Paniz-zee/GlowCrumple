using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public string recipeName = "Bread"; // Name of the recipe
    public int requiredFlour = 3;
    public int requiredEggs = 2;
    public int requiredButter = 1;

    public void TryToBake()
    {
        // Check if player has all the required ingredients
        if (InventoryController.Instance.HasItem("Flour") && InventoryController.Instance.GetItemCount("Flour") >= requiredFlour &&
            InventoryController.Instance.HasItem("Eggs") && InventoryController.Instance.GetItemCount("Eggs") >= requiredEggs &&
            InventoryController.Instance.HasItem("Butter") && InventoryController.Instance.GetItemCount("Butter") >= requiredButter)
        {
            // Remove the required ingredients
            InventoryController.Instance.RemoveItem("Flour", requiredFlour);
            InventoryController.Instance.RemoveItem("Eggs", requiredEggs);
            InventoryController.Instance.RemoveItem("Butter", requiredButter);

            Debug.Log(recipeName + " baked successfully!");
        }
        else
        {
            Debug.Log("Missing ingredients to bake " + recipeName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && InventoryController.Instance != null)
        {
            TryToBake();
        }
    }
}
