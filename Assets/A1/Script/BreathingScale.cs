using UnityEngine;

public class BreathingScale : MonoBehaviour
{
    public float speed = 3f;
    public float amount = 0.06f;

    private Vector3 originalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = 1f + Mathf.Sin(Time.time * speed) * amount;
        transform.localScale = originalScale * scale;
    }
}
