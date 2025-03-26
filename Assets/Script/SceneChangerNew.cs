using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangerNew : MonoBehaviour
{
    public void ChangeSceneToGame()
    {
        // Load the "GameScene" by its name
        SceneManager.LoadScene("GameScene");
    }

  
}
