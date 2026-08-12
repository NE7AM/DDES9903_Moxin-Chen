using UnityEngine;

public class RevealAfterLeave : MonoBehaviour
{
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
