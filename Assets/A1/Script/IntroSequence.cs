using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class IntroSequence : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource bgmSource;

    public AudioClip motherVoice;
    public AudioClip fatherVoice;
    public AudioClip doorSlam;

    public CanvasGroup blackCanvas;
    public Volume blurVolume;

    public float startDelay = 1f;
    public float doorSoundTime = 0.9f;
    public float blackFadeTime = 1.5f;
    public float blurClearTime = 2f;

    private IEnumerator Start()
    {
        blackCanvas.alpha = 1f;
        blackCanvas.blocksRaycasts = true;
        blurVolume.weight = 1f;

        yield return new WaitForSeconds(startDelay);

        yield return PlayVoice(motherVoice);
        yield return PlayVoice(fatherVoice);

        audioSource.PlayOneShot(doorSlam);
        yield return new WaitForSeconds(doorSoundTime);

        yield return FadeBlack();
        yield return ClearBlur();

        blackCanvas.blocksRaycasts = false;
        blackCanvas.gameObject.SetActive(false);

        if (bgmSource != null)
        {
            bgmSource.Play();
        }
    }

    private IEnumerator PlayVoice(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
    }

    private IEnumerator FadeBlack()
    {
        float time = 0f;

        while (time < blackFadeTime)
        {
            time += Time.deltaTime;

            blackCanvas.alpha = Mathf.Lerp(
                1f,
                0f,
                time / blackFadeTime
            );

            yield return null;
        }

        blackCanvas.alpha = 0f;
    }

    private IEnumerator ClearBlur()
    {
        float time = 0f;

        while (time < blurClearTime)
        {
            time += Time.deltaTime;

            blurVolume.weight = Mathf.Lerp(
                1f,
                0f,
                time / blurClearTime
            );

            yield return null;
        }

        blurVolume.weight = 0f;
    }
}