using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] MusicLibrary musicLibrary;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource UiSFX_Source;

    public void PlayMusic(string trackName, float fadeDuration = 0.5f) => StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));

    public void PlayUiSFX(string sfxName) => PlayUiSFX(musicLibrary.GetClipFromName(sfxName));

    void PlayUiSFX(AudioClip nextTrack)
    {
        UiSFX_Source.clip = nextTrack;
        UiSFX_Source.Play();
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        if (musicSource.clip != nextTrack)
        {
            float percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicSource.volume = Mathf.Lerp(.08f, 0, percent);
                yield return null;
            }

            musicSource.clip = nextTrack;
            musicSource.Play();

            percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicSource.volume = Mathf.Lerp(0, .08f, percent);
                yield return null;
            }
        }
    }
}
