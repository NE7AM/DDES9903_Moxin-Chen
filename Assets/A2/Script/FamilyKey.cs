using UnityEngine;
using System.Collections;

public class FamilyKey : MonoBehaviour
{
    public GameObject key;
    public AudioSource keySound;
    public Behaviour doorInteraction;

    bool active;
    bool started;
    Quaternion startRotation;

    void Start()
    {
        startRotation = key.transform.localRotation;
        doorInteraction.enabled = false;
    }

    void Update()
    {
        if (!active)
            return;

        float swing = Mathf.Sin(Time.time * 5f) * 12f;

        key.transform.localRotation =
            startRotation * Quaternion.Euler(0f, 0f, swing);
    }

    public void StartKeyTimer()
    {
        if (!started)
            StartCoroutine(KeyTimer());
    }

    IEnumerator KeyTimer()
    {
        started = true;

        yield return new WaitForSeconds(10f);

        active = true;
        keySound.Play();
    }

    public void TakeKey()
    {
        active = false;
        keySound.Stop();
        key.SetActive(false);

        doorInteraction.enabled = true;
    }
}