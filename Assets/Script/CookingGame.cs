using UnityEngine;
using UnityEngine.UI; // Import this for the Button component
using System.Collections; // Add this to use IEnumerator and coroutines

public class CookingGame : MonoBehaviour
{
    public GameObject bowl;   // Reference to the bowl object
    public GameObject spoon;  // Reference to the spoon (which is now a button)
    private bool ingredient1Added = false;
    private bool ingredient2Added = false;

    // The speed of the spoon movement
    public float moveSpeed = 5f;

    // This function will be called when the spoon (acting as the button) is clicked
    public void MixIngredients()
    {
        if (ingredient1Added && ingredient2Added)
        {
            Debug.Log("Mixing ingredients!");
            StartCoroutine(MoveSpoonToBowl());
        }
        else
        {
            Debug.Log("Add ingredients before mixing!");
        }
    }

    private IEnumerator MoveSpoonToBowl()
    {
        Vector3 originalPosition = spoon.transform.position;
        Vector3 targetPosition = bowl.transform.position; // Move towards the bowl

        float journeyLength = Vector3.Distance(originalPosition, targetPosition);
        float startTime = Time.time;

        while (Vector3.Distance(spoon.transform.position, targetPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            spoon.transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            // Debugging line to see the progress of the movement
            Debug.Log($"Moving Spoon: {fractionOfJourney * 100}%");

            yield return null; // Wait until the next frame
        }

        Debug.Log("Spoon reached the bowl!");
        // You can optionally call another coroutine to return the spoon to the original position if needed.
    }

    // Optionally, move the spoon back to its original position after mixing
    private IEnumerator MoveSpoonBack(Vector3 originalPosition)
    {
        Vector3 currentPosition = spoon.transform.position;

        float journeyLength = Vector3.Distance(currentPosition, originalPosition);
        float startTime = Time.time;

        while (Vector3.Distance(spoon.transform.position, originalPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            spoon.transform.position = Vector3.Lerp(currentPosition, originalPosition, fractionOfJourney);
            yield return null; // Wait until the next frame
        }
    }
}
