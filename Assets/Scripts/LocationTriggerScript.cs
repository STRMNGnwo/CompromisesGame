    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LocationTriggerScript : MonoBehaviour
    {
        //this script is attached to specific doorways, that are used to set boolean variables in the PlayerScript.
        public GameObject PlayerReference;
        PlayerScript pscript;
        // Start is called before the first frame update
        void Start()
        {
            PlayerReference=GameObject.FindGameObjectWithTag("PLAYER");
            pscript=PlayerReference.GetComponent<PlayerScript>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //The function below is used by the necessary doors in the house(for now) to change the boolean values in the PlayerScript.
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Location Triggers working");
            
            if(this.gameObject.name=="KitchenDoor")
            {
                // Debug.Log("Player is now in Kitchen");
                pscript.setInKitchen();
            }

            else if(this.gameObject.name=="LivingRoomDoor")
            {
                //Debug.Log("Player is now in Living Room");
                pscript.setInLivingRoom();
            }

            else if( this.gameObject.name=="BedroomDoor")
            {
                //Debug.Log("Player is now in the Bedroom");
                pscript.setInBedroom();
            }

            else if (this.gameObject.name=="OfficeTrigger")
            {
                Debug.Log("Player is now in the Office");
                pscript.setInOffice();
            }
            
        }

        private void OnTriggerExit2D(Collider2D other) //the OfficeTrigger is a trigger that spans almost the entire office, so the OnExit is required
        {
            if(this.gameObject.name=="OfficeTrigger")
            {
                Debug.Log("Player is not working");
                pscript.setInOffice();
            }
            
        }
    }
