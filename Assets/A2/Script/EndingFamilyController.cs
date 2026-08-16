using UnityEngine;
using System.Collections;

public class EndingFamilyController : MonoBehaviour
{
    public StationApproachTrigger stationApproach;

    public HomeVoiceTrigger homeVoice;
    public AudioSource finalInner;

    public CanvasGroup whiteScreen;
    public CanvasGroup blackScreen;

    public float whiteHoldDuration = 1.5f;
    public float fadeToBlackDuration = 2f;

    bool started;

    void Start()
    {
        whiteScreen.alpha = 0f;
        blackScreen.alpha = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (started || !other.CompareTag("Player"))
            return;

        started = true;
        StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        // No new wind gusts.
        // Current wind can finish naturally.
        stationApproach.StopWindGusts();

        // Mother can finish current sentence,
        // but will not repeat again.
        homeVoice.PrepareForEnding();

        while (homeVoice.IsVoicePlaying())
            yield return null;

        // Instant white screen
        whiteScreen.alpha = 1f;

        // Final inner starts
        finalInner.Play();

        // Hold white screen
        yield return new WaitForSeconds(whiteHoldDuration);

        // Fade from white to black
        float time = 0f;

        while (time < fadeToBlackDuration)
        {
            time += Time.deltaTime;

            blackScreen.alpha =
                Mathf.Lerp(0f, 1f, time / fadeToBlackDuration);

            yield return null;
        }

        blackScreen.alpha = 1f;
    }
}