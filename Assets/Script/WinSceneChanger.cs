using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinSceneChanger : MonoBehaviour
{
   
    public void ChangeSceneToStart()
    {
        // Stop the music before changing the scene
        //MusicManager.instance.StopMusic();

        SceneManager.LoadScene("StartScene");
    }
}
