using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneChanger : MonoBehaviour
{
    // Call this method to stop the music and load the StartScene
    public void ChangeSceneToStart()
    {
        // Stop the music before changing the scene
        MusicManager.instance.StopMusic();

        // Load the StartScene
        SceneManager.LoadScene("StartScene");
    }
}
