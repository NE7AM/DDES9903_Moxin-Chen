using UnityEngine;

public class MemoryBubbleState : MonoBehaviour
{
    public MemoryType memoryType;

    void Start()
    {
        if (StoryState.Instance.IsVisited(memoryType))
        {
            gameObject.SetActive(false);
        }
    }
}