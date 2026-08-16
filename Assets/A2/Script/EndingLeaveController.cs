using UnityEngine;
using System.Collections;

public class EndingLeaveController : MonoBehaviour
{
    public StationApproachTrigger stationApproach;

    public GameObject glassDoor;

    public Transform trainStationWorld;
    public Transform parkFacilityWorld;

    public AudioSource trainSound;
    public AudioSource finalInner;

    public CanvasGroup blackScreen;

    public float moveDuration = 6f;
    public float trainFadeDuration = 2f;

    bool started;

    void Start()
    {
        blackScreen.alpha = 0f;
    }

    public void StartEnding()
    {
        if (started)
            return;

        started = true;
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        // Stop future wind gusts
        stationApproach.StopWindGusts();

        // Close glass door
        glassDoor.SetActive(true);

        // Train sound starts first
        float startTrainVolume = trainSound.volume;
        trainSound.Play();

        // Wait 3 seconds before movement
        yield return new WaitForSeconds(3f);

        // World starts moving and inner starts together
        StartCoroutine(MoveWorld());
        finalInner.Play();

        // After 4.5 seconds, train sound fades out
        yield return new WaitForSeconds(4.5f);

        float time = 0f;

        while (time < trainFadeDuration)
        {
            time += Time.deltaTime;

            trainSound.volume = Mathf.Lerp(
                startTrainVolume,
                0f,
                time / trainFadeDuration
            );

            yield return null;
        }

        trainSound.Stop();

        // Wait until inner monologue finishes
        while (finalInner.isPlaying)
            yield return null;

        yield return new WaitForSeconds(1f);

        // Fade to black
        time = 0f;

        while (time < 2f)
        {
            time += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(0f, 1f, time / 2f);
            yield return null;
        }

        blackScreen.alpha = 1f;
    }

    IEnumerator MoveWorld()
    {
        Vector3 stationStart = trainStationWorld.position;
        Vector3 parkStart = parkFacilityWorld.position;

        Vector3 stationEnd = stationStart + new Vector3(0f, 0f, -40f);
        Vector3 parkEnd = parkStart + new Vector3(0f, 0f, -40f);

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            trainStationWorld.position =
                Vector3.Lerp(stationStart, stationEnd, t);

            parkFacilityWorld.position =
                Vector3.Lerp(parkStart, parkEnd, t);

            yield return null;
        }
    }
}