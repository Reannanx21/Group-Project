using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] CanvasGroup fadeCanvasGroup; // Reference to the CanvasGroup for fading

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

    // Method to load the next level
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Loading Next Level: " + (currentSceneIndex + 1));
        StartCoroutine(LoadLevel(currentSceneIndex + 1));  // Load the next scene
    }

    // Method to load the previous level
    public void PreviousLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Loading Previous Level: " + (currentSceneIndex - 1));
        if (currentSceneIndex > 0)
        {
            StartCoroutine(LoadLevel(currentSceneIndex - 1));  // Load the previous scene
        }
        else
        {
            Debug.Log("No previous level available.");
        }
    }

    // Coroutine for loading a scene with a fade effect
    IEnumerator LoadLevel(int sceneIndex)
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup not assigned!");
            yield break;
        }

        // Fade Out
        yield return StartCoroutine(Fade(1f));  // Fade out over 1 second

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is loaded
        }

        // Fade In after the scene has finished loading
        yield return StartCoroutine(Fade(0f));  // Fade in over 1 second
    }

    // Fade function to handle fade in and fade out
    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0f;
        float duration = 1f;  // Duration of the fade in seconds

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;  // Ensure we set it exactly to the target value
    }
}