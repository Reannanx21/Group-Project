using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuController : MonoBehaviour
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
    public void OnControlsClick()
    {
        SceneManager.LoadScene("Controls");
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnRestartClick()
    {
        SceneManager.LoadScene("MainMenu");
        

    }
}
