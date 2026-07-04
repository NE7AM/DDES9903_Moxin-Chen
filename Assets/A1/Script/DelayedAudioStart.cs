using UnityEngine;

public class DelayedAudioStart : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 1.5f;

    private void Start()
    {
        audioSource.PlayDelayed(delay);
    }
}