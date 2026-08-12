using UnityEngine;

public class SimpleFootsteps : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private float moveThreshold = 0.08f;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;

        footstepSource.loop = true;
        footstepSource.Play();
        footstepSource.mute = true;
    }

    void Update()
    {
        Vector3 delta = transform.position - lastPosition;
        delta.y = 0f;

        float speed = delta.magnitude / Mathf.Max(Time.deltaTime, 0.0001f);

        footstepSource.mute = speed <= moveThreshold;
        lastPosition = transform.position;
    }
}