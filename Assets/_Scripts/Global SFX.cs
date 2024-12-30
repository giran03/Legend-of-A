using UnityEngine;

public class GlobalSFX : MonoBehaviour
{
    public Sound sfx_pop;

    public void PlayPopSFX() => sfx_pop.Play(transform.position);
}
