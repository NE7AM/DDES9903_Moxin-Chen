using UnityEngine;
using System.Collections;

public class StationVoiceTrigger : MonoBehaviour
{
    public AudioSource voice;

    static bool voicePlaying = false;

    bool played = false;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!played && !voicePlaying)
            StartCoroutine(PlayVoice());
    }

    IEnumerator PlayVoice()
    {
        played = true;
        voicePlaying = true;

        voice.Play();

        while (voice.isPlaying)
            yield return null;

        voicePlaying = false;
    }
}