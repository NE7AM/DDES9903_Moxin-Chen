using UnityEngine;
using System.Collections;

public class BedroomProgressController : MonoBehaviour
{
    [Header("Train")]
    public Collider trainCollider;
    public GameObject trainGlow;
    public AudioSource trainActivateSound;

    [Header("Inner")]
    public AudioSource diaryInner;
    public AudioSource photoInner;
    public AudioSource suitcaseInner;

    bool diaryDone;
    bool photoDone;
    bool suitcaseDone;

    bool unlocking;
    bool unlocked;

    void Start()
    {
        trainCollider.enabled = false;
        trainGlow.SetActive(false);

        StartCoroutine(UnlockTimer());
    }

    IEnumerator UnlockTimer()
    {
        yield return new WaitForSeconds(45f);
        TryUnlockTrain();
    }

    public void CompleteDiary()
    {
        if (diaryDone) return;

        diaryDone = true;
        CheckProgress();
    }

    public void CompletePhoto()
    {
        if (photoDone) return;

        photoDone = true;
        CheckProgress();
    }

    public void CompleteSuitcase()
    {
        if (suitcaseDone) return;

        suitcaseDone = true;
        CheckProgress();
    }

    void CheckProgress()
    {
        if (diaryDone && photoDone && suitcaseDone)
            TryUnlockTrain();
    }

    void TryUnlockTrain()
    {
        if (unlocked || unlocking)
            return;

        unlocking = true;
        StartCoroutine(UnlockTrain());
    }

    IEnumerator UnlockTrain()
    {
        while (diaryInner.isPlaying ||
               photoInner.isPlaying ||
               suitcaseInner.isPlaying)
        {
            yield return null;
        }

        unlocked = true;

        trainGlow.SetActive(true);
        trainActivateSound.Play();

        trainCollider.enabled = true;
    }
}