using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource;
    public AudioClip backgroundMusic;  // Single music for all scenes

    void Awake()
    {
        // Ensure that the music manager is persistent across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent music manager from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances of the music manager
            return;
        }

        // Get the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();

        // If music isn't already playing, start playing it
        if (!audioSource.isPlaying)
        {
            PlayBackgroundMusic();
        }

        // Subscribe to scene loaded events to handle changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Make sure the background music continues to play without interruption
        if (!audioSource.isPlaying)
        {
            PlayBackgroundMusic();
        }
    }

    void PlayBackgroundMusic()
    {
        if (audioSource.clip != backgroundMusic)
        {
            audioSource.clip = backgroundMusic;  // Set the music clip
            audioSource.loop = true;  // Loop the music
            audioSource.Play();  // Start the music
        }
    }

    // Optional: Stop the music when needed (for instance, at the end of the game)
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
