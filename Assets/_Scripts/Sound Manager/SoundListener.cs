using UnityEngine;

public class SoundListener : MonoBehaviour
{
    [SerializeField] private AudioListener audioListener;

    void Start()
    {
        if (audioListener)
        {
            SingletonHandler.soundManager.SetListener(audioListener.transform);
        }
    }
}