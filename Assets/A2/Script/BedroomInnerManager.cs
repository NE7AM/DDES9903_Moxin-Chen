using UnityEngine;
using System.Collections;

public class BedroomInnerManager : MonoBehaviour
{
    [Header("Inner")]
    public AudioSource diaryInner;
    public AudioSource photoInner;
    public AudioSource suitcaseInner;

    [Header("Suitcase")]
    public AudioSource zipperSound;

    [Header("Interact Objects")]
    public GameObject photoInteract;
    public GameObject suitcaseInteract;

    [Header("Progress")]
    public BedroomProgressController progress;

    bool photoDone;
    bool suitcaseDone;
    bool zipperPlayed;

    bool AnyInnerPlaying()
    {
        return diaryInner.isPlaying ||
               photoInner.isPlaying ||
               suitcaseInner.isPlaying;
    }

    public void TryPhoto()
    {
        if (photoDone || AnyInnerPlaying())
            return;

        photoDone = true;

        photoInner.Play();
        progress.CompletePhoto();

        photoInteract.SetActive(false);
    }

    public void TrySuitcase()
    {
        if (suitcaseDone)
            return;

        if (AnyInnerPlaying())
        {
            if (!zipperPlayed)
            {
                zipperPlayed = true;
                zipperSound.Play();
            }

            return;
        }

        if (!zipperPlayed)
        {
            zipperPlayed = true;
            StartCoroutine(ZipperThenInner());
        }
        else
        {
            PlaySuitcaseInner();
        }
    }

    IEnumerator ZipperThenInner()
    {
        zipperSound.Play();

        yield return new WaitForSeconds(zipperSound.clip.length);

        if (!AnyInnerPlaying())
            PlaySuitcaseInner();
    }

    void PlaySuitcaseInner()
    {
        suitcaseDone = true;

        suitcaseInner.Play();
        progress.CompleteSuitcase();

        suitcaseInteract.SetActive(false);
    }
}