using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (item != null)
        {
            item.transform.SetParent(transform);
            // Update the cooking ingredients
            FindObjectOfType<CookingStation>().AddIngredient(item.name);
        }
    }

}
