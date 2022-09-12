using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{
    [SerializeField]
    //public Scene goToScene;
    public string goToScene;
    //public string previousScene;

    public static bool recentlyMovedScenes = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MoveToScene Script is working");

        Debug.Log("Current Scene is: "+SceneManager.GetActiveScene().name);
        Debug.Log("Scenes in Build Indexes:"+SceneManager.sceneCountInBuildSettings);

        GameManagerScript.Instance.previousScene=SceneManager.GetActiveScene().name;

        Debug.Log("Set Game Manager Previous Scene to: "+GameManagerScript.Instance.previousScene);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
  
    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log("Collision Detected. Trying to change scene");
        
         //the conditional statement is not used anymore
        if(!recentlyMovedScenes) {
            recentlyMovedScenes = true;
        }  
         SceneManager.LoadScene(goToScene);   //actually changing the scene. 

        Debug.Log("Recently Moved Scenes so not moving");  
        //if the Player GameObject collides with this collider, load another scene.
    }

   /*  private void OnTriggerExit2D(Collider2D other) {
        recentlyMovedScenes = false;

    }*/
}
