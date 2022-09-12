using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExistingStuff : MonoBehaviour
{
    //this script is used to attach duplicate copies of the gameObjects that may have been carried over by DontDestroyOnLoad.
    // Start is called before the first frame update
    void Start()
    {
        //destroy the objects in Don't destroy on load, if they still exist.

        DestroyImmediate(GameObject.FindGameObjectWithTag("PLAYER"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("PAUSEMENUCANVAS"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("MONEYCANVAS"));
        
        DestroyImmediate(GameObject.FindGameObjectWithTag("STOCKMARKETCANVAS"));
        DestroyImmediate(GameObject.Find("TheStockMarketCanvas(Clone)"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("GAMEMANAGER"));
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
