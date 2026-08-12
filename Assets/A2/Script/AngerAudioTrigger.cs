using UnityEngine;
using System.Collections;

public class AngerAudioTrigger : MonoBehaviour
{
    public AudioSource conflictAudio;
    public AudioSource innerMonologue;

    public float fadeDuration = 2.5f;
    public float delayBeforeMonologue = 0.5f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        StartCoroutine(FadeAndPlayMonologue());
    }

    private IEnumerator FadeAndPlayMonologue()
    {
        float startVolume = conflictAudio.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            conflictAudio.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        conflictAudio.Stop();
        conflictAudio.volume = startVolume;

        yield return new WaitForSeconds(delayBeforeMonologue);

        innerMonologue.Play();
    }
}