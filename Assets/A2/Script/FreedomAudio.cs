using UnityEngine;
using System.Collections;

public class FreedomAudio : MonoBehaviour
{
    public AudioSource innerVoice;
    public AudioSource birds;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        innerVoice.Play();

        yield return new WaitForSeconds(12f);

        float startVolume = birds.volume;

        for (float t = 0; t < 3f; t += Time.deltaTime)
        {
            birds.volume = Mathf.Lerp(startVolume, 0f, t / 3f);
            yield return null;
        }

        birds.Stop();
    }
}