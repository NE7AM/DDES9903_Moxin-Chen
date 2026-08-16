using UnityEngine;

public class StoryItemStateSetter : MonoBehaviour
{
    public void GetAwayTicket()
    {
        if (StoryState.Instance != null)
            StoryState.Instance.GetAwayTicket();
    }

    public void GetHouseKey()
    {
        if (StoryState.Instance != null)
            StoryState.Instance.GetHouseKey();
    }
}