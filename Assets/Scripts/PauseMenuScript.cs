using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In the Pause Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButtonClicked()
    {
     GameManagerScript.isGamePaused=false;
     Time.timeScale=1f;
     SceneManager.LoadScene("StartMenu");

    }

    public void ResumeButtonClicked()
    {
        GameManagerScript.isGamePaused=false;
        ResumeGame();
    }

    /*public void MenuButtonClicked()
    {
        SceneManager.LoadScene("StartMenu");
    }*/

    public void ResumeGame()
    {
       // isGamePaused=false;
       this.gameObject.SetActive(false);
        Time.timeScale=1f;
    }

    public void PauseGame()
    {
        //isGamePaused=true;
        this.gameObject.SetActive(true);
        Time.timeScale=0f; //stopping the in-game time.
    }
}
