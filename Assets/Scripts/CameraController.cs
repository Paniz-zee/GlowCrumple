using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player; // Assign the player GameObject
    [SerializeField] float smoothSpeed = 5f; // Smooth movement speed
    [SerializeField] float xOffset; // Offset to position the camera properly

    void Update()
    {
        if (player != null)
        {
            // Only follow the player's X position (horizontal movement)
            Vector3 targetPosition = new Vector3(player.position.x + xOffset, transform.position.y, transform.position.z);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
