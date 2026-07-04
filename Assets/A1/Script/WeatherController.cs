using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Material darkSkybox;
    public Light sunLight;
    public Light lightningLight;

    public AudioSource thunderSource;
    public AudioSource darkBgmSource;
    public AudioSource secondBroadcastSource;

    public float transitionTime = 6f;
    public float thunderTime = 1.8f;
    public float secondBroadcastDelay = 1.8f;

    public float darkSunnyExposure = 0.28f;
    public Color darkSunnyTint = new Color32(95, 100, 115, 255);

    public float darkSunIntensity = 0.2f;
    public float darkAmbientIntensity = 0.3f;

    public Color darkFogColor = new Color32(57, 57, 70, 255);
    public float darkFogDensity = 0.018f;

    private bool stormStarted;
    private Material sunnySkybox;

    public void StartStorm()
    {
        if (stormStarted)
        {
            return;
        }

        stormStarted = true;

        sunnySkybox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = sunnySkybox;

        StartCoroutine(ChangeWeather());
    }

    private IEnumerator ChangeWeather()
    {
        float startExposure = sunnySkybox.GetFloat("_Exposure");
        Color startTint = sunnySkybox.GetColor("_Tint");

        float startSunIntensity = sunLight.intensity;
        float startAmbientIntensity = RenderSettings.ambientIntensity;
        float startFogDensity = RenderSettings.fogDensity;

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogColor = darkFogColor;

        float timer = 0f;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;

            float amount = Mathf.SmoothStep(
                0f,
                1f,
                timer / transitionTime
            );

            sunnySkybox.SetFloat(
                "_Exposure",
                Mathf.Lerp(
                    startExposure,
                    darkSunnyExposure,
                    amount
                )
            );

            sunnySkybox.SetColor(
                "_Tint",
                Color.Lerp(
                    startTint,
                    darkSunnyTint,
                    amount
                )
            );

            sunLight.intensity = Mathf.Lerp(
                startSunIntensity,
                darkSunIntensity,
                amount
            );

            RenderSettings.ambientIntensity = Mathf.Lerp(
                startAmbientIntensity,
                darkAmbientIntensity,
                amount
            );

            RenderSettings.fogDensity = Mathf.Lerp(
                startFogDensity,
                darkFogDensity,
                amount
            );

            yield return null;
        }

        yield return StartCoroutine(ChangeSkyWithLightning());
    }

    private IEnumerator ChangeSkyWithLightning()
    {
        if (thunderSource != null)
        {
            thunderSource.Play();
        }

        if (secondBroadcastSource != null)
        {
            secondBroadcastSource.PlayDelayed(secondBroadcastDelay);
        }

        lightningLight.enabled = true;

        yield return new WaitForSeconds(0.12f);

        RenderSettings.skybox = darkSkybox;

        lightningLight.enabled = false;

        yield return new WaitForSeconds(0.18f);

        lightningLight.enabled = true;

        yield return new WaitForSeconds(0.08f);

        lightningLight.enabled = false;

        float remainingTime = thunderTime - 0.38f;

        if (remainingTime > 0f)
        {
            yield return new WaitForSeconds(remainingTime);
        }

        if (darkBgmSource != null)
        {
            darkBgmSource.Play();
        }
    }
}