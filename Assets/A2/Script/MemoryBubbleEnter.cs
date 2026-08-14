using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryBubbleEnter : MonoBehaviour
{
    public MemoryType memoryType;
    public string sceneName;

    public void EnterMemory()
    {
        if (StoryState.Instance.IsVisited(memoryType))
            return;

        SceneManager.LoadScene(sceneName);
    }
}