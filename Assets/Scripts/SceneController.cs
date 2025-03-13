using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Add this
using System.Collections.Generic; // Not needed for IEnumerator, but useful for Lists

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

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

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        if (transitionAnim == null)
        {
            Debug.LogError("Animator not assigned!");
            yield break;
        }

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        transitionAnim.SetTrigger("start");
    }
}
