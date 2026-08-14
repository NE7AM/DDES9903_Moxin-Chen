using UnityEngine;

public class BubbleBreathing : MonoBehaviour
{
    Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float pulse = 1f + Mathf.Sin(Time.time * 1.3f) * 0.1f;
        transform.localScale = startScale * pulse;
    }
}