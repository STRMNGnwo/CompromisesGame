using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCanvasScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //used to position the stats bars above the player at all times.
        this.gameObject.transform.position=Player.transform.position+Player.GetComponent<PlayerScript>().UIOffset;
        
    }
}
