using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TiledLevelScript : MonoBehaviour
{
    [SerializeField] private Tilemap[] tileMaps;
    [SerializeField] private TileBase[] tileBases; // This time, loading is dynamic.
    [SerializeField] private char[] tileKeys;
    [SerializeField] private char[] tileObstacles;
    private int rows; // Y-axis.
    private int cols; // X-axis.

    [SerializeField] private Color targetColor; // Serialized for debug purposes.
    [SerializeField] private Color[] targetColors; // Array of target colors to transition to.
    [SerializeField] private float transitionDuration; // Duration of the transition in seconds. Warning: do not set too low! You've been warned.
    [SerializeField] private float pauseDuration;

    [SerializeField] public GameObject pitfallZone; // Assign in Inspector
    [SerializeField] private float tileSize = 1f;
    [SerializeField] public GameObject checkpointPrefab;
    [SerializeField] public GameObject finishPrefab;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(ChangeBackgroundColor());
        LoadLevel();

        // Update PitfallZone collider with level width
        if (pitfallZone != null)
        {
            BoxCollider2D pitCollider = pitfallZone.GetComponent<BoxCollider2D>();
            pitCollider.size = new Vector2((cols + 4f) * tileSize, (rows + 2f) * tileSize); // X size = level width, Y size = level height
            pitCollider.offset = new Vector2(((cols + 2f) * tileSize) / 2f, ((rows - 2f) * tileSize) / -2f);
        }
    }

    private void LoadLevel()
    {
        try
        {
            // Load the TileBases.
            LoadAndSortTileBases();
            // Load tile data first.
            using (StreamReader reader = new StreamReader("Assets/TileData.txt"))
            {
                // Read all tile chars and create an array from it.
                string line = reader.ReadLine();
                tileKeys = line.ToCharArray();
                // Next is the obstacle tiles.
                line = reader.ReadLine();
                tileObstacles = line.ToCharArray();
                // We can also do the hazards. Next time.
            }
            // Then load level data.
            using (StreamReader reader = new StreamReader("Assets/Level2.txt"))
            {
                GetRowsAndColumns();
                string line;
                for (int row = 1; row < rows + 1; row++)
                {
                    line = reader.ReadLine();
                    for (int col = 0; col < cols; col++)
                    {
                        char c = line[col];
                        if (c == '*') continue; // Skip if sky tile.

                        // Check if it's a checkpoint
                        if (c == 'C')
                        {
                            if (checkpointPrefab != null)
                            {
                                Vector3 spawnPosition = new Vector3(col + 0.5f, -row + 0.5f, 0);
                                Instantiate(checkpointPrefab, spawnPosition, Quaternion.identity);
                            }
                            continue;
                        }
                        else if (c == 'F')
                        {
                            if (finishPrefab != null)
                            {
                                Vector3 spawnPosition = new Vector3(col + 0.5f, -row + 0.5f, 0);
                                Instantiate(finishPrefab, spawnPosition, Quaternion.identity);
                            }
                            continue;
                        }

                        int charIndex = Array.IndexOf(tileKeys, c);
                        if (charIndex == -1) throw new Exception("Index not found: " + c);
                        // Check if tile is obstacle or normal.
                        if (Array.IndexOf(tileObstacles, c) > -1) // Tile is obstacle.
                        {
                            SetTile(0, charIndex, col, row);
                            Debug.Log("Setting tile");
                        }
                        else // Tile is normal.
                        {
                            SetTile(1, charIndex, col, row);
                            Debug.Log("Setting tile");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void GetRowsAndColumns()
    {
        // Read all lines from the text file.
        string[] lines = File.ReadAllLines("Assets/Level2.txt");

        // Check if any lines were read
        if (lines.Length == 0) return;
        rows = lines.Length;
        cols = lines[0].Length;
    }

    private void SetTile(int tileMapIndex, int charIndex, int col, int row)
    {
        // Check all tilemaps to see if there's a manually-painted tile there.
        foreach (Tilemap tilemap in tileMaps)
        {
            if (tilemap.HasTile(new Vector3Int(col, -row, 0))) return;
        }
        // If no tile, then set the tile in the desired tilemap.
        tileMaps[tileMapIndex].SetTile(new Vector3Int(col, -row, 0), tileBases[charIndex]);
    }

    private void LoadAndSortTileBases()
    {
        tileBases = Resources.LoadAll<TileBase>("TileBases");
        Array.Sort(tileBases, (x, y) => ExtractNumber(x.name).CompareTo(ExtractNumber(y.name)));
        //Array.Sort(tileBases, CompareTileNames);
    }

    int ExtractNumber(string name)
    {
        return Int32.Parse(new string(name.Where(Char.IsDigit).ToArray()));
    }

    // Below are longer versions of the sort, which may be easier to understand.

    //private int CompareTileNames(TileBase x, TileBase y)
    //{
    //    int numberX = ExtractNumber(x.name);
    //    int numberY = ExtractNumber(y.name);
    //    return numberX.CompareTo(numberY);
    //}

    //private int ExtractNumber(string name)
    //{
    //    string numberString = "";
    //    foreach (char c in name)
    //    {
    //        if (Char.IsDigit(c))
    //        {
    //            numberString += c;
    //        }
    //    }
    //    return Int32.Parse(numberString);
    //}

    IEnumerator ChangeBackgroundColor()
    {
        while (true)
        {
            // Start with the current background color.
            Color startColor = cam.backgroundColor;

            do // Set a target color.
            {
                targetColor = targetColors[UnityEngine.Random.Range(0, targetColors.Length)];
            }
            while (targetColor == startColor);

            // Calculate the color increment for each channel.
            float rIncrement = (targetColor.r - startColor.r) / transitionDuration;
            float gIncrement = (targetColor.g - startColor.g) / transitionDuration;
            float bIncrement = (targetColor.b - startColor.b) / transitionDuration;

            // Transition to the target color.
            float elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                Color newColor = new Color(
                    startColor.r + rIncrement * elapsedTime,
                    startColor.g + gIncrement * elapsedTime,
                    startColor.b + bIncrement * elapsedTime,
                    cam.backgroundColor.a
                );
                cam.backgroundColor = newColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the camera's background color to the target color.
            cam.backgroundColor = targetColor;

            // Wait for a brief pause before repeating the process.
            yield return new WaitForSeconds(pauseDuration);
        }
    }


}
