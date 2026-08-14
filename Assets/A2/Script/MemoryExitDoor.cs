using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryExitDoor : MonoBehaviour
{
    public MemoryType memoryType;

    public void ExitMemory()
    {
        Debug.Log("ExitMemory called: " + memoryType);

        if (StoryState.Instance == null)
        {
            Debug.LogError("StoryState.Instance is NULL!");
            return;
        }

        StoryState.Instance.VisitMemory(memoryType);

        Debug.Log("Visited: " + memoryType);
        Debug.Log("Visited Count: " + StoryState.Instance.VisitedCount());

        if (StoryState.Instance.VisitedCount() >= 2)
            SceneManager.LoadScene("TrainStation");
        else
            SceneManager.LoadScene("Grassland");
    }
}