using UnityEngine;

public class RevealAfterLeave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject diaryText;
    public GameObject trainGlow;

    private bool hasRevealed;

    private void Awake()
    {
        if (diaryText != null)
        {
            diaryText.SetActive(false);
        }

        if (trainGlow != null)
        {
            trainGlow.SetActive(false);
        }
    }

    public void Reveal()
    {
        if (hasRevealed)
        {
            return;
        }

        hasRevealed = true;

        if (diaryText != null)
        {
            diaryText.SetActive(true);
        }

        if (trainGlow != null)
        {
            trainGlow.SetActive(true);
        }
    }
}
