using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    //this script is attached to the Spawn Game Object in each scene, to set a spawn point for the player.
    // Start is called before the first frame update
     //public GameObject PlayerReference;
    void Start()
    {
        //if the previous scene was the office, set the spawn position outside it, otherwise, spawn as per normal.
        if(GameManagerScript.Instance.previousScene=="OfficeInterior")
        {
            Debug.Log("Previous scene was the Office ");
            GameObject.FindGameObjectWithTag("PLAYER").transform.position= new Vector2(10,-7);
        }
        else
      GameObject.FindGameObjectWithTag("PLAYER").transform.position=this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
