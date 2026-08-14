using UnityEngine;

public class FreedomBubbleFloat : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 startScale;

    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    void Update()
    {
        float move = Mathf.Sin(Time.time * 1.5f) * 0.35f;
        float pulse = 1f + Mathf.Sin(Time.time * 1.2f) * 0.08f;

        transform.position = startPosition + Vector3.up * move;
        transform.localScale = startScale * pulse;
    }
}