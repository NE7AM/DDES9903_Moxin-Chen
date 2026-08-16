using UnityEngine;
using System.Collections;

public class EndingFreedomController : MonoBehaviour
{
    public StationApproachTrigger stationApproach;

    [Header("Train")]
    public Transform train;
    public Transform trainEndPoint;
    public AudioSource trainSound;

    [Header("Ending")]
    public AudioSource finalInner;
    public CanvasGroup blackScreen;

    [Header("Sky")]
    public Light sun;
    public float finalSunIntensity = 0.4f;
    public float finalSkyExposure = 0.65f;

    [Header("Timing")]
    public float trainMoveDelay = 2.7f;
    public float trainMoveDuration = 6f;
    public float trainFadeStart = 4.5f;
    public float trainFadeDuration = 2f;

    bool started;

    void Start()
    {
        blackScreen.alpha = 0f;
    }

    public void StartEnding()
    {
        if (started)
            return;

        started = true;
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        // No new wind gusts
        stationApproach.StopWindGusts();

        // Train sound starts immediately
        float startVolume = trainSound.volume;
        trainSound.Play();

        // Train sound fades after 4.5 seconds
        StartCoroutine(FadeTrainSound(startVolume));

        // Wait before train moves and inner starts
        yield return new WaitForSeconds(trainMoveDelay);

        // Train movement and inner start together
        StartCoroutine(MoveTrain());
        finalInner.Play();

        // Wait until inner finishes
        while (finalInner.isPlaying)
            yield return null;

        // Fade to black immediately
        float time = 0f;

        while (time < 2f)
        {
            time += Time.deltaTime;

            blackScreen.alpha =
                Mathf.Lerp(0f, 1f, time / 2f);

            yield return null;
        }

        blackScreen.alpha = 1f;
    }

    IEnumerator MoveTrain()
    {
        Vector3 startPosition = train.position;

        Material skybox = RenderSettings.skybox;

        float startSun = sun.intensity;
        float startExposure = 0f;

        if (skybox != null && skybox.HasProperty("_Exposure"))
            startExposure = skybox.GetFloat("_Exposure");

        float time = 0f;

        while (time < trainMoveDuration)
        {
            time += Time.deltaTime;
            float t = time / trainMoveDuration;

            // Train moves away
            train.position = Vector3.Lerp(
                startPosition,
                trainEndPoint.position,
                t
            );

            // Environment becomes slightly brighter
            sun.intensity = Mathf.Lerp(
                startSun,
                finalSunIntensity,
                t
            );

            if (skybox != null && skybox.HasProperty("_Exposure"))
            {
                skybox.SetFloat(
                    "_Exposure",
                    Mathf.Lerp(
                        startExposure,
                        finalSkyExposure,
                        t
                    )
                );
            }

            yield return null;
        }

        DynamicGI.UpdateEnvironment();
    }

    IEnumerator FadeTrainSound(float startVolume)
    {
        yield return new WaitForSeconds(trainFadeStart);

        float time = 0f;

        while (time < trainFadeDuration)
        {
            time += Time.deltaTime;

            trainSound.volume =
                Mathf.Lerp(
                    startVolume,
                    0f,
                    time / trainFadeDuration
                );

            yield return null;
        }

        trainSound.volume = 0f;
        trainSound.Stop();
    }
}