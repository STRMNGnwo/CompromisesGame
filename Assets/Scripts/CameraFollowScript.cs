using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    //this component is attached to the camera in The Outside scene, and lets the camera follow the player.
    // Start is called before the first frame update
    public Transform Player;
    public float smoothingValue;
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();//accessing the transform of the player gameobject
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(this.transform.position!=Player.transform.position)//if the camera is not following the player, set its position to match the player's position
        {
            Vector3 targetPosition=new Vector3(Player.position.x,Player.position.y,this.transform.position.z);
            
            float minX=-11.26f;
            float maxX=11.92f;
            float minY=-5.56f;
            float maxY=6.76f;
            //making sure that the camera does not go out of bounds of the scene.
            targetPosition.x=Mathf.Clamp(targetPosition.x,minX,maxX);
            targetPosition.y=Mathf.Clamp(targetPosition.y,minY,maxY);
            
            this.transform.position= Vector3.Lerp(this.transform.position,targetPosition,smoothingValue);

        } 
    }
}
