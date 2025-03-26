using UnityEngine;
using UnityEngine.UI;  // To access UI components like Button

public class BowlMovement : MonoBehaviour
{
    public Button bakeButton; // Reference to the Bake button
    public float moveSpeed = 5f; // Speed at which the bowl moves
    private bool isMoving = false;

    void Start()
    {
        // Ensure the Bake button triggers the MoveBowlDown method when clicked
        bakeButton.onClick.AddListener(MoveBowlDown);
    }

    void Update()
    {
        // If the bowl is moving, update its position
        if (isMoving)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    void MoveBowlDown()
    {
        isMoving = true;
    }
}
