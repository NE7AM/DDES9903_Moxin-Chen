using System.Collections;
using UnityEngine;

public class EndingSequence : MonoBehaviour
{
    public LivingRoomIntro livingRoomIntro;

    public AudioSource innerVoice;
    public AudioSource bgmSource;

    public float pauseBeforeVoice = 0.3f;
    public float pauseAfterVoice = 1f;
    public float bgmFadeTime = 2f;

    private bool hasStarted;

    private void OnTriggerEnter(Collider other)
    {
        if (hasStarted || !other.CompareTag("Player"))
        {
            return;
        }

        hasStarted = true;
        StartCoroutine(PlayEnding());
    }

    private IEnumerator PlayEnding()
    {
        while (!livingRoomIntro.DialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(pauseBeforeVoice);

        innerVoice.Play();

        yield return new WaitForSeconds(innerVoice.clip.length);
        yield return new WaitForSeconds(pauseAfterVoice);

        float startVolume = bgmSource.volume;
        float timer = 0f;

        while (timer < bgmFadeTime)
        {
            timer += Time.deltaTime;

            bgmSource.volume = Mathf.Lerp(
                startVolume,
                0f,
                timer / bgmFadeTime
            );

            yield return null;
        }

        bgmSource.Stop();
    }
}