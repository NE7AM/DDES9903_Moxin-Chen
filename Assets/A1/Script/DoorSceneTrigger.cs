using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorSceneTrigger : MonoBehaviour
{
    public string nextSceneName;

    public bool isLoading;

    public void OnTriggerEnter(Collider other)
    {
        if (isLoading)
        {
            return;
        }

        if (other.CompareTag("Player") ||
            other.transform.root.CompareTag("Player"))
        {
            isLoading = true;
            SceneManager.LoadSceneAsync(nextSceneName);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
