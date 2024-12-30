using UnityEngine;

public class SingletonHandler : MonoBehaviour
{
    public static SoundManager soundManager { get; private set; }
    public static MusicManager musicManager { get; private set; }
    public static GlobalSFX globalSFX { get; private set; }
    public static SceneHandler sceneHandler { get; private set; }

    public static SingletonHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        soundManager = GetComponent<SoundManager>();
        musicManager = GetComponent<MusicManager>();
        globalSFX = GetComponent<GlobalSFX>();
        sceneHandler = GetComponent<SceneHandler>();

        Debug.Log("Checking all components...");

        if (soundManager != null)
        {
            Debug.Log("SoundManager component exists");
        }
        else
        {
            Debug.LogError("SoundManager component does not exist");
        }

        if (musicManager != null)
        {
            Debug.Log("MusicManager component exists");
        }
        else
        {
            Debug.LogError("MusicManager component does not exist");
        }

        if (globalSFX != null)
        {
            Debug.Log("GlobalSFX component exists");
        }
        else
        {
            Debug.LogError("GlobalSFX component does not exist");
        }

        if (sceneHandler != null)
        {
            Debug.Log("SceneHandler component exists");
        }
        else
        {
            Debug.LogError("SceneHandler component does not exist");
        }
    }
}
