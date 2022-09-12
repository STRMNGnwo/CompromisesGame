using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string goToScene; // variable that stores the name of the scene that has to be transitioned to.
    void Start()
    {
       
    }

   //this function is primarily used by menu buttons, when they have to trigger a transition to a new menu that is a scene.
   public void ButtonClick()
    {
        Debug.Log("A Button in the Menu was Clicked");
        SceneManager.LoadScene(goToScene);

    }

    //used to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
