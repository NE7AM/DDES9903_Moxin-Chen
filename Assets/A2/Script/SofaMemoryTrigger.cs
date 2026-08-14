using UnityEngine;
using System.Collections;

public class SofaMemoryTrigger : MonoBehaviour
{
    public FamilyInnerManager innerManager;
    public GameObject glowShell;

    bool triggered;

    void Start()
    {
        glowShell.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        innerManager.TriggerInner1();
        StartCoroutine(FlashGlow());
    }

    IEnumerator FlashGlow()
    {
        while (true)
        {
            glowShell.SetActive(true);
            yield return new WaitForSeconds(0.6f);

            glowShell.SetActive(false);
            yield return new WaitForSeconds(0.6f);
        }
    }
}