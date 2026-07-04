using System.Collections;
using UnityEngine;

public class StationArrivalSequence : MonoBehaviour
{
    public AudioSource stationBroadcast;
    public AudioSource doorKnock;
    public AudioSource motherVoice;
    public AudioSource fatherVoice;

    public GameObject returnTicketGuide;
    public float delayAfterBroadcast = 3f;

    private bool hasStarted;

    private void OnTriggerEnter(Collider other)
    {
        if (hasStarted || !other.CompareTag("Player"))
        {
            return;
        }

        hasStarted = true;
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        stationBroadcast.Play();
        yield return new WaitForSeconds(stationBroadcast.clip.length);

        yield return new WaitForSeconds(delayAfterBroadcast);

        doorKnock.Play();
        yield return new WaitForSeconds(doorKnock.clip.length);

        motherVoice.Play();
        yield return new WaitForSeconds(motherVoice.clip.length);

        fatherVoice.Play();
        yield return new WaitForSeconds(fatherVoice.clip.length);

        returnTicketGuide.SetActive(true);
    }
}