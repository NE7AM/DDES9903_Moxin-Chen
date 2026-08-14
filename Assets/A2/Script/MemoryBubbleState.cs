using UnityEngine;

public class MemoryBubbleState : MonoBehaviour
{
    public MemoryType memoryType;

    public GameObject normalBubble;
    public GameObject dimShell;
    public GameObject glow;
    public Behaviour interaction;
    public Collider bubbleCollider;

    void Start()
    {
        bool visited = StoryState.Instance.IsVisited(memoryType);

        if (visited)
        {
            normalBubble.SetActive(false);
            dimShell.SetActive(true);
            glow.SetActive(false);

            interaction.enabled = false;
            bubbleCollider.enabled = false;
        }
        else
        {
            normalBubble.SetActive(true);
            dimShell.SetActive(false);
            glow.SetActive(true);

            interaction.enabled = true;
            bubbleCollider.enabled = true;
        }
    }
}