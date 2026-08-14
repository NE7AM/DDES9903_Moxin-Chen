using UnityEngine;

public class BookshelfMemoryTrigger : MonoBehaviour
{
    public FamilyInnerManager innerManager;
    public GameObject lightObject;

    bool triggered;

    void Start()
    {
        lightObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;

        innerManager.TriggerInner3();
        lightObject.SetActive(true);
    }
}