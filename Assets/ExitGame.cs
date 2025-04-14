using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour
{

    private void Start()
    {
        
    }

  public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}

   
