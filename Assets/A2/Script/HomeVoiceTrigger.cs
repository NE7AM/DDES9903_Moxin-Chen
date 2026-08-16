using UnityEngine;
using System.Collections;

public class HomeVoiceTrigger : MonoBehaviour
{
    public AudioSource motherVoice;
    public float repeatDelay = 5f;

    bool playerInside;
    bool allowRepeat = true;
    Coroutine voiceLoop;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;

        if (allowRepeat && voiceLoop == null)
            voiceLoop = StartCoroutine(VoiceLoop());
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;

        if (allowRepeat)
            motherVoice.Stop();
    }

    IEnumerator VoiceLoop()
    {
        while (playerInside && allowRepeat)
        {
            motherVoice.Play();

            while (motherVoice.isPlaying)
                yield return null;

            if (!playerInside || !allowRepeat)
                break;

            yield return new WaitForSeconds(repeatDelay);
        }

        voiceLoop = null;
    }

    // Ending starts:
    // current voice can finish, but it will never repeat
    public void PrepareForEnding()
    {
        allowRepeat = false;
    }

    public bool IsVoicePlaying()
    {
        return motherVoice.isPlaying;
    }
}