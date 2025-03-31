using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    public bool goToNextLevel = true; // Toggle this in the inspector to choose next or previous level

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    IEnumerator LoadLevelCoroutine()
    {
        if (transitionAnim == null)
        {
            Debug.LogError("Animator not assigned!");
            yield break;
        }

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1); // Wait for animation to finish

        int targetSceneIndex;

        if (goToNextLevel)
        {
            targetSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // Load next scene
        }
        else
        {
            targetSceneIndex = SceneManager.GetActiveScene().buildIndex - 1; // Load previous scene
        }

        // Ensure the scene index is within the valid range
        if (targetSceneIndex < 0 || targetSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("No scene available in this direction.");
            yield break;
        }

        // Asynchronously load the target scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        transitionAnim.SetTrigger("start"); // Trigger the "Start" transition after loading the scene
    }
}