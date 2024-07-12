using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;


    public void Pause()
    {
        
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Home()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Resume()
    {
        
        pauseMenu?.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Resstart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
