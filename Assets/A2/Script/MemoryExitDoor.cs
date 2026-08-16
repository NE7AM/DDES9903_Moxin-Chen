using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryExitDoor : MonoBehaviour
{
    public MemoryType memoryType;

    public void ExitMemory()
    {
        StoryState.Instance.VisitMemory(memoryType);

        Debug.Log("Exit from: " + memoryType);
        Debug.Log("Anger: " + StoryState.Instance.angerVisited);
        Debug.Log("Family: " + StoryState.Instance.familyVisited);
        Debug.Log("Freedom: " + StoryState.Instance.freedomVisited);
        Debug.Log("Count: " + StoryState.Instance.VisitedCount());

        if (StoryState.Instance.VisitedCount() >= 2)
        {
            Debug.Log("Loading TrainStation");
            SceneManager.LoadScene("TrainStation");
        }
        else
        {
            Debug.Log("Loading Grassland");
            SceneManager.LoadScene("Grassland");
        }
    }
}