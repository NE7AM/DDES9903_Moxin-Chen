using UnityEngine;

public class TrainGlowController : MonoBehaviour
{
    [Header("Light Reference")]
    [SerializeField] private Light glowLight;

    [Header("Pulse Settings")]
    [SerializeField] private float minIntensity = 0.4f;
    [SerializeField] private float maxIntensity = 2.0f;
    [SerializeField] private float pulseDuration = 1.8f;

    [Header("Initial State")]
    [SerializeField] private bool startActive = false;

    private bool isActive;

    private void Awake()
    {
        isActive = startActive;
        ApplyCurrentState();
    }

    private void Update()
    {
        if (!isActive || glowLight == null)
        {
            return;
        }

        float phase = Time.time * Mathf.PI * 2f / pulseDuration;
        float pulseValue = (Mathf.Sin(phase) + 1f) * 0.5f;

        glowLight.intensity =
            Mathf.Lerp(minIntensity, maxIntensity, pulseValue);
    }

    public void ActivateGlow()
    {
        isActive = true;

        if (glowLight != null)
        {
            glowLight.enabled = true;
        }
    }

    public void DeactivateGlow()
    {
        isActive = false;

        if (glowLight != null)
        {
            glowLight.intensity = 0f;
            glowLight.enabled = false;
        }
    }

    private void ApplyCurrentState()
    {
        if (glowLight == null)
        {
            Debug.LogWarning(
                "TrainGlowController: No Light has been assigned.",
                this
            );

            return;
        }

        glowLight.enabled = isActive;

        if (!isActive)
        {
            glowLight.intensity = 0f;
        }
    }
}

