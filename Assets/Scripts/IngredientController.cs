using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public string ingredientName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.AddItem(ingredientName);
            Destroy(gameObject);
        }
    }
}
