using System.Collections;
using UnityEngine;

public class RepeatSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public float repeatDelay = 2.5f;

    private Coroutine repeatCoroutine;

    public void StartRepeating()
    {
        if (repeatCoroutine != null)
        {
            return;
        }

        repeatCoroutine = StartCoroutine(RepeatSound());
    }

    public void StopSound()
    {
        if (repeatCoroutine != null)
        {
            StopCoroutine(repeatCoroutine);
            repeatCoroutine = null;
        }

        audioSource.Stop();
    }

    private IEnumerator RepeatSound()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(
                audioSource.clip.length + repeatDelay
            );
        }
    }
}