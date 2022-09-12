using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //variable for controlling player movement speed.
    public float movementSpeed=0;
    public Animator animator;
    public Vector3 UIOffset;
    //reference to the player's rigidbody2d component
    public Rigidbody2D PlayerRigidBody;

    //reference to the HealthBar in the stats canvas
    public GameObject HEALTHBAR;
    public GameObject FATIGUEBAR;
    public GameObject MENTALSTATEBAR;

    public GameObject MONEYPANEL;

    bool isDead=false;
    public bool inBedroom=false,inLivingRoom=false,inOffice=false,inKitchen=false;

    //player's status bars.
    double healthBar=100.0,mentalStateBar=100.0,fatigueBar=100.0; // mechanic, if bar gets to 0, starts affecting health bar decrease rate. 
    //amount of money the player starts out with.
    long moneyBar=3000;
    double fatigueBarDecreaseRate, fatigueBarIncreaseRate,healthBarIncreaseRate, healthBarDecreaseRate, mentalStateBarIncreaseRate,mentalStateBarDecreaseRate;

    //hashtable that stores the stocks that the player has, if any.

    public Hashtable stocks;

    // Start is called before the first frame update
    void Start()
    {
        //don't destroy this gameObject when loading another scene.
        DontDestroyOnLoad(this.gameObject);

        MONEYPANEL=GameObject.FindGameObjectWithTag("MONEYCANVAS");
        MONEYPANEL.GetComponent<MoneyScript>().setValue((int)moneyBar);

        PlayerRigidBody=GetComponent<Rigidbody2D>();
         //assuming the player is created already and his position is initialised.
         UIOffset= new Vector3(this.gameObject.transform.position.x,(this.gameObject.transform.position.y-2f),this.gameObject.transform.position.z); 

         //initialising the player stocks hashtable.

         stocks=new Hashtable();
         
         InvokeRepeating("checkPlayerStats",0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        Move();
    }

    void Move()
    {
        //getting which directional key was pressed
        float x=Input.GetAxisRaw("Horizontal");
        float y= Input.GetAxisRaw("Vertical");

         //conditional statement to flip the player based on which key was pressed.
        if(x==-1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else if(x==1){
              transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


        //check if the shift key is being pressed.If it is, increase movement speed by 3, causing the player to run

        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift))
        {
            //Debug.Log("Sprinting now. Fatigue increasing as well");
            movementSpeed=8;
            
            //decrease the fatigueBar here as sprinting should make the character more tired.
            fatigueBar-=1*Time.deltaTime;
            
        }
        //movement speed reset
        else if( (x==1||x==-1)|| (y==1||y==-1) ){
            movementSpeed=3;
        }
        else{
            movementSpeed=0;
        }

        //sending the movement speed to the animator in order to trigger animation transitions, if necessary.
        animator.SetFloat("MovementSpeed",movementSpeed);

        //normalising the vector, so that the player is not faster when diagonal movement is performed.
        Vector2 directionToMove= new Vector2(x,y).normalized;

        PlayerRigidBody.velocity=new Vector2(movementSpeed * directionToMove.x, movementSpeed * directionToMove.y);
    
    }

     //used to continously check the stats of the player and update them based on location.
     //if any of the booleans are false, reduce the corresponding value by a certain amount.
     //if any of the booleans are true, increase the corresponding value by a certain amount.
    void checkPlayerStats()
    {
        //if the player is not dead.
    if(!isDead)
        {
            string SceneName=SceneManager.GetActiveScene().name;
           // Debug.Log("Current scene is: "+ SceneName);
        
        //setting the increase and decrease rates of the bars based on their current level.
        setFatigueBarMechanics();
        setHealthBarMechanics();
        setMentalStateBarMechanics();

        //if the health is less than or equal to 0, stop player movement, play death animation and set isDead to be true.
        if(healthBar<=0)
        {
            Debug.Log("Player DEAD, GAME OVER");
            isDead=true;
            animator.SetBool("isDead",true);
            PlayerRigidBody.velocity= new Vector2(0,0); //stopping player movement after death.
           // GameManagerScript.Instance.GameOver();
        }
        
        if(inBedroom && string.Equals(SceneName,"HouseInterior") && fatigueBar<100)
        {
           // Debug.Log("Changing stats, player in Bedroom");
            healthBar-=(1*healthBarDecreaseRate);
            fatigueBar+=(1*fatigueBarIncreaseRate);
            mentalStateBar-=(1*mentalStateBarDecreaseRate);
        }

        else if(inKitchen && string.Equals(SceneName,"HouseInterior") && healthBar<100)
        {
            Debug.Log("Health: "+healthBar);
            healthBar+=(1*healthBarIncreaseRate);
            fatigueBar-=(1*fatigueBarDecreaseRate);
            mentalStateBar-=(1*mentalStateBarDecreaseRate);

            if(mentalStateBar<=0||fatigueBarDecreaseRate<=0) //if mental state bar or fatigue bar is zero, decrease health regardless of location.
            {
                healthBar-=healthBarDecreaseRate;
            }
        }

        else if(inOffice && string.Equals(SceneName,"OfficeInterior"))
        {
           // Debug.Log("Changing Stats, player in Office");
            //increment the money bar by 100, every second if the player is in the office.
            MONEYPANEL.GetComponent<MoneyScript>().payWage(100);
            healthBar-=(1*healthBarDecreaseRate);
            fatigueBar-=(1*fatigueBarDecreaseRate);
            mentalStateBar-=(1*mentalStateBarDecreaseRate);
        }

        else if(inLivingRoom && string.Equals(SceneName,"HouseInterior") && mentalStateBar<100)
        {
           //Debug.Log("Changing stats, player in LivingRoom");
           mentalStateBar+=(1*mentalStateBarIncreaseRate);
           healthBar-=(1*healthBarDecreaseRate);
           fatigueBar-=(1*fatigueBarDecreaseRate);
        }

        else{ //if the player is not in any of the rooms(would be in the City or in the house foyer), decrease all bars.
           
            healthBar-=healthBarDecreaseRate;
            fatigueBar-=fatigueBarDecreaseRate;
            mentalStateBar-=mentalStateBarDecreaseRate;
        }

        //Debug.Log("Health: "+healthBar);
       // Debug.Log("Fatigue: "+fatigueBar );
        // Debug.Log("Mental State: "+mentalStateBar);

        //Actually setting the values in the UI
        HEALTHBAR.GetComponent<StatsBarScript>().setValue(healthBar);
        FATIGUEBAR.GetComponent<StatsBarScript>().setValue(fatigueBar);
        MENTALSTATEBAR.GetComponent<StatsBarScript>().setValue(mentalStateBar);

    }

    }
    
    //functions that are called when the appropriate location trigger is hit.
    public void setInKitchen()
    {
        inKitchen=(!inKitchen);
        Debug.Log("Is the Player in the Kitchen?: "+inKitchen);
    }

    public void setInBedroom()
    {
        inBedroom=(!inBedroom);
        
        Debug.Log("Is the Player in the bedroom?: "+inBedroom );
    }

    public void setInOffice()
    {
        inOffice=(!inOffice);

        Debug.Log("Is the Player in the Office?: "+ inOffice);
    }

    public void setInLivingRoom()
    {
        inLivingRoom=(!inLivingRoom);
        Debug.Log("Is the Player in the LivingRoom? : "+inLivingRoom);
    }

    public void setFatigueBarMechanics()
    {
       // Debug.Log("Setting FatigueBar Mechanics");

         if(fatigueBar<=0)
         {
            Debug.Log("Player is extremely fatigued");
            fatigueBar=0;// stop it from going into the negatives.
            healthBar-=0.5;//healthBar decreases faster if the fatigue bar is empty.
         }
         else if(fatigueBar<=100&&fatigueBar>=50) //player is well rested
        {
            fatigueBarIncreaseRate=2;
            fatigueBarDecreaseRate=0.5;
        }
        else if(fatigueBar<50&&fatigueBar>25)//player is quite tired, fatigue meter starts decreasing fast.
        {
            fatigueBarIncreaseRate=1.5;
            fatigueBarDecreaseRate=1;
        }
       else if(fatigueBar<=25){
    
           fatigueBarIncreaseRate=1;
           fatigueBarDecreaseRate=1;
       }
    }

    public void setHealthBarMechanics()
    {
       //Debug.Log("Setting HealthBar mechanics");

       if(healthBar<=100 && healthBar>=50)
       {
           healthBarIncreaseRate=2;        
           healthBarDecreaseRate=1;
       }

       else if(healthBar<50&&healthBar>25)
       {
           healthBarIncreaseRate=1.5;
           healthBarDecreaseRate=1;
       }

       else if(healthBar<=25){
           healthBarIncreaseRate=1;
           healthBarDecreaseRate=1;
       }
    }

    public void setMentalStateBarMechanics()
    {
       if(mentalStateBar<=0)
       {
           Debug.Log("Player is mentally tired");
           mentalStateBar=0;
           healthBar-=0.5; //if mentalStateBar is at rock bottom, start affecting the healthBar.
       }
       else if(mentalStateBar<=100&&mentalStateBar>=70)
       {
           mentalStateBarIncreaseRate=2;        
           mentalStateBarDecreaseRate=0.5;
       }

       else if(mentalStateBar<50 && mentalStateBar>=25)
        {
            mentalStateBarIncreaseRate=1.5;
            mentalStateBarDecreaseRate=1;
        }
       else if(mentalStateBar<25){
           mentalStateBarIncreaseRate=1;
           mentalStateBarDecreaseRate=1;
       }
    }

    //utility method that other Scripts can use to check if the Player is dead.
    public bool isPlayerDead()
    {
        return isDead;

    }
}
