using UnityEngine;

public class BreathingLight : MonoBehaviour
{
    public Light[] lights;
    public float speed = 1.2f;
    public float minIntensity = 0.25f;

    float[] startIntensity;
    bool blinking = true;

    void Start()
    {
        startIntensity = new float[lights.Length];

        for (int i = 0; i < lights.Length; i++)
            startIntensity[i] = lights[i].intensity;
    }

    void Update()
    {
        if (!blinking)
            return;

        float pulse = (Mathf.Sin(Time.time * speed) + 1f) / 2f;

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = Mathf.Lerp(
                startIntensity[i] * minIntensity,
                startIntensity[i],
                pulse
            );
        }
    }

    public void StopBlinking()
    {
        blinking = false;

        for (int i = 0; i < lights.Length; i++)
            lights[i].intensity = startIntensity[i];
    }
}