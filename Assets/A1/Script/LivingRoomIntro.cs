using System.Collections;
using UnityEngine;

public class LivingRoomIntro : MonoBehaviour
{
    public CanvasGroup whiteCanvas;

    public AudioSource bgmSource;
    public AudioSource motherVoice1;
    public AudioSource fatherVoice;
    public AudioSource motherVoice2;

    public float whiteHoldTime = 0.8f;
    public float fadeTime = 1.5f;

    public bool DialogueFinished { get; private set; }

    private IEnumerator Start()
    {
        DialogueFinished = false;

        whiteCanvas.alpha = 1f;
        whiteCanvas.blocksRaycasts = true;

        yield return new WaitForSeconds(whiteHoldTime);

        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            whiteCanvas.alpha = Mathf.Lerp(
                1f,
                0f,
                timer / fadeTime
            );

            yield return null;
        }

        whiteCanvas.alpha = 0f;
        whiteCanvas.blocksRaycasts = false;
        whiteCanvas.gameObject.SetActive(false);

        bgmSource.Play();

        yield return PlayVoice(motherVoice1);
        yield return PlayVoice(fatherVoice);
        yield return PlayVoice(motherVoice2);

        DialogueFinished = true;
    }

    private IEnumerator PlayVoice(AudioSource voice)
    {
        voice.Play();
        yield return new WaitForSeconds(voice.clip.length);
    }
}