using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endscreen : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
