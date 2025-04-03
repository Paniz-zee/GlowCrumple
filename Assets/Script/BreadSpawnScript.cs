using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class BreadSpawnScript : MonoBehaviour
{
    public GameObject breadPrefab;   // The bread prefab to spawn
    public Transform spawnPosition;  // Position where the bread will appear (above the bowl)
    public Transform tablePosition;  // Position where the bread will be placed (on the table)
    public float moveSpeed = 2f;     // Speed at which the bread moves
    public TMP_Text messageText;
    public string winSceneName = "WinScene"; // Change this to your actual scene name

    private bool breadIsSpawned = false; // Prevent multiple spawns

    void Start()
    {
        if (breadPrefab == null || spawnPosition == null || tablePosition == null)
        {
            Debug.LogError("Please assign all references in the Inspector.");
            return;
        }
    }

    public void StartBaking()
    {
        if (breadIsSpawned) return;  // Prevent multiple spawns

        StartCoroutine(SpawnBreadAfterDelay(2f));
    }

    private IEnumerator SpawnBreadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        GameObject bread = Instantiate(breadPrefab, spawnPosition.position, Quaternion.identity);
        breadIsSpawned = true;

        while (Vector3.Distance(bread.transform.position, tablePosition.position) > 0.1f)
        {
            bread.transform.position = Vector3.MoveTowards(bread.transform.position, tablePosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        bread.transform.position = tablePosition.position;
        messageText.text = "Your bread is ready!";

        yield return new WaitForSeconds(2f); // Wait 2 seconds before scene transition

        SceneManager.LoadScene(winSceneName); // Load the Win Scene
    }
}
