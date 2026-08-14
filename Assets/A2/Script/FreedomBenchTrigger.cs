using UnityEngine;
using System.Collections;

public class FreedomBenchTrigger : MonoBehaviour
{
    public AudioSource firstInner;
    public AudioSource secondInner;
    public AudioSource dogBark;
    public AudioSource thirdInner;
    public AudioSource bubbleAudio;
    public Behaviour dogMovement;

    public float bubbleTargetVolume = 0.2f;

    bool triggered;

    void Start()
    {
        bubbleAudio.volume = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        // Wait until the first inner monologue has finished
        while (firstInner.isPlaying)
            yield return null;

        // Second inner
        secondInner.Play();

        while (secondInner.isPlaying)
            yield return null;

        // Wait 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Dog bark
        dogBark.Play();

        while (dogBark.isPlaying)
            yield return null;

        // Wait 0.8 seconds
        yield return new WaitForSeconds(0.8f);

        // Third inner and dog start moving together
        thirdInner.Play();
        dogMovement.enabled = true;

        // Wait 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Start bubble sound
        bubbleAudio.Play();

        // Fade bubble sound in over 2 seconds
        float time = 0f;

        while (time < 2f)
        {
            time += Time.deltaTime;
            bubbleAudio.volume =
                Mathf.Lerp(0f, bubbleTargetVolume, time / 2f);

            yield return null;
        }

        bubbleAudio.volume = bubbleTargetVolume;
    }
}