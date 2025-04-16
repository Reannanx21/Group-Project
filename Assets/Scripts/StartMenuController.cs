using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuController : MonoBehaviour
{
    private Animator anim;

    
    public void OnStartClick()
    {
        anim = GetComponent<Animator>();
        SceneManager.LoadScene("Level 1 The Joys of Piracy");
        anim.SetTrigger("StartGame");
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
        anim = GetComponent<Animator>();
        SceneManager.LoadScene("Controls");
        anim.SetTrigger("ControlFade");
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
