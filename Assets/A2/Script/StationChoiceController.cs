using UnityEngine;

public class StationChoiceController : MonoBehaviour
{
    [Header("Train")]
    public GameObject trainGlass;
    public GameObject trainArrow;
    public GameObject noTicketTrigger;
    public GameObject boardTrainTrigger;

    [Header("Home")]
    public GameObject homeLight;
    public GameObject homeDoorTrigger;
    public GameObject homeEndingPlane;

    void Start()
    {
        if (StoryState.Instance == null)
        {
            Debug.LogError("StoryState not found!");
            return;
        }

        bool ticket = StoryState.Instance.hasAwayTicket;
        bool key = StoryState.Instance.hasHouseKey;

        // Train
        trainArrow.SetActive(true);

        trainGlass.SetActive(!ticket);
        noTicketTrigger.SetActive(!ticket);
        boardTrainTrigger.SetActive(ticket);

        // Home
        homeLight.SetActive(key);
        homeDoorTrigger.SetActive(key);
        homeEndingPlane.SetActive(key);
    }
}