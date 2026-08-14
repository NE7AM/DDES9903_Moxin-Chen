using UnityEngine;
using System.Collections;

public class FamilyInnerManager : MonoBehaviour
{
    public AudioSource inner1;
    public AudioSource inner2;
    public AudioSource inner3;

    public FamilyKey familyKey;

    bool requested1;
    bool requested2;
    bool requested3;

    bool played1;
    bool played2;
    bool played3;

    bool busy;

    public void TriggerInner1()
    {
        if (requested1 || played1)
            return;

        requested1 = true;
        TryPlay();
    }

    public void TriggerInner2()
    {
        // Remote does nothing until Inner1 has completely finished
        if (!played1 || requested2 || played2)
            return;

        requested2 = true;
        TryPlay();
    }

    public void TriggerInner3()
    {
        if (requested3 || played3)
            return;

        requested3 = true;
        TryPlay();
    }

    void TryPlay()
    {
        if (busy)
            return;

        if (requested1 && !played1)
        {
            StartCoroutine(PlayAudio(inner1, 1));
        }
        else if (requested2 && played1 && !played2)
        {
            StartCoroutine(PlayAudio(inner2, 2));
        }
        else if (requested3 && !played3)
        {
            StartCoroutine(PlayAudio(inner3, 3));
        }
    }

    IEnumerator PlayAudio(AudioSource audio, int number)
    {
        busy = true;

        audio.Play();

        // Start the key timer when the first memory audio actually plays
        familyKey.StartKeyTimer();

        while (audio.isPlaying)
            yield return null;

        // Mark as completed immediately when the audio finishes
        if (number == 1) played1 = true;
        if (number == 2) played2 = true;
        if (number == 3) played3 = true;

        yield return new WaitForSeconds(0.3f);

        busy = false;
        TryPlay();
    }
}