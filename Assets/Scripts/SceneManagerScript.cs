using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;  //resume if paused
        } 
    }
    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
