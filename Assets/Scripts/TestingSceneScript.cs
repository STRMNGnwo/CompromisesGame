using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSceneScript : MonoBehaviour
{
    //this script is not part of the game, and is purely used for testing functionality in a separate test scene.
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //Loading the Player object.
         Player=(GameObject)GameObject.FindGameObjectWithTag("PLAYER");

         Player.transform.position=new Vector2(0,0);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
