using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Level 1 The Joys of Piracy");

    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }
}