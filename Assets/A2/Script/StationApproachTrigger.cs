using UnityEngine;
using System.Collections;

public class StationApproachTrigger : MonoBehaviour
{
    public Light sun;
    public GameObject groundLights;
    public AudioSource windAudio;
    public AudioSource stationBroadcast;

    public float fadeDuration = 5f;
    public float windInterval = 7f;
    public float finalSkyExposure = 0.15f;

    bool triggered;
    bool windEnabled;

    float startSun;
    float startAmbient;
    float startExposure;

    Material skybox;

    void Start()
    {
        groundLights.SetActive(false);

        startSun = sun.intensity;
        startAmbient = RenderSettings.ambientIntensity;

        if (RenderSettings.skybox != null)
        {
            skybox = new Material(RenderSettings.skybox);
            RenderSettings.skybox = skybox;

            if (skybox.HasProperty("_Exposure"))
                startExposure = skybox.GetFloat("_Exposure");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        windEnabled = true;

        StartCoroutine(WindLoop());
        StartCoroutine(FadeToDark());
        StartCoroutine(StationSequence());
    }

    IEnumerator WindLoop()
    {
        while (windEnabled)
        {
            windAudio.Play();

            yield return new WaitForSeconds(
                windAudio.clip.length + windInterval
            );
        }
    }

    IEnumerator StationSequence()
    {
        yield return new WaitForSeconds(1f);
        groundLights.SetActive(true);

        yield return new WaitForSeconds(1f);
        stationBroadcast.Play();
    }

    IEnumerator FadeToDark()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            // Main sunlight fades completely
            sun.intensity = Mathf.Lerp(startSun, 0f, t);

            // Environment becomes much darker
            RenderSettings.ambientIntensity =
                Mathf.Lerp(startAmbient, startAmbient * 0.15f, t);

            // Skybox becomes dark
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

        sun.intensity = 0f;
        RenderSettings.ambientIntensity = startAmbient * 0.15f;

        if (skybox != null && skybox.HasProperty("_Exposure"))
            skybox.SetFloat("_Exposure", finalSkyExposure);

        DynamicGI.UpdateEnvironment();
    }

    public void StopWindGusts()
    {
        windEnabled = false;
    }
}