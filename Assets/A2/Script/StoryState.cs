using UnityEngine;

public enum MemoryType
{
    Anger,
    Family,
    Freedom
}

public class StoryState : MonoBehaviour
{
    public static StoryState Instance;

    public bool angerVisited;
    public bool familyVisited;
    public bool freedomVisited;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void VisitMemory(MemoryType type)
    {
        if (type == MemoryType.Anger)
            angerVisited = true;

        if (type == MemoryType.Family)
            familyVisited = true;

        if (type == MemoryType.Freedom)
            freedomVisited = true;
    }

    public bool IsVisited(MemoryType type)
    {
        if (type == MemoryType.Anger)
            return angerVisited;

        if (type == MemoryType.Family)
            return familyVisited;

        return freedomVisited;
    }

    public int VisitedCount()
    {
        int count = 0;

        if (angerVisited) count++;
        if (familyVisited) count++;
        if (freedomVisited) count++;

        return count;
    }
}