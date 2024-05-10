using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    //should be readonly whenever possible
    public static SceneControl Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }
    public void NextScene()
    {
        // Get the index of the next scene in build settings
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if there's a scene at the next index
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available.");
        }
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}