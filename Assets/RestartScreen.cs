using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScreen : MonoBehaviour
{
    public void Reset()
    {
        SceneManager.LoadScene("ReloadScene");
    }
}
