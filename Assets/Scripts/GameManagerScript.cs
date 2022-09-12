using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public string previousScene; //used to keep track of which scene the transition occurred from. This is used to set a secondary spawn point in scenes that require them.
    public static bool isGamePaused=false;
    public static bool isStockMarketOpen=false; 
    public bool isGameOver=false;
    private static GameObject PauseMenu;
    private static GameObject GameOverScreen;

    private static GameManagerScript _instance; 
    
    //the Game Manager is a Singleton, so other scripts are provided an Instance of it to access.
    public static GameManagerScript Instance
    {
      get{
          if(_instance==null)
          {
              Debug.Log("Game Manager does not exist yet");
          }
          return _instance;
      }
    } //other game objects and scenes can access the GameManager via this.

    private GameObject Player;
    private GameObject MoneyUI;
    private GameObject StockMarket;
    
   
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting the GameManager");

        //instantiating the Pause Menu.
        PauseMenu=(GameObject)GameObject.Instantiate(Resources.Load("PauseMenuCanvas"));
        PauseMenu=GameObject.FindGameObjectWithTag("PAUSEMENUCANVAS");
        PauseMenu.SetActive(false);

        //Instantiating the Player.
        Player=(GameObject) GameObject.Instantiate(Resources.Load("Player"));  
         Debug.Log("Player Game Object created");      
       
       // Instantiating the Money and the Stock Market UI;
        MoneyUI=(GameObject) GameObject.Instantiate(Resources.Load("MoneyContainerCanvas"));
        StockMarket=(GameObject) GameObject.Instantiate(Resources.Load("TheStockMarketCanvas"));
        StockMarket.SetActive(false);

       //Don't destroy the created UI GameObjects, when a new scene is loaded.
        DontDestroyOnLoad(PauseMenu);
        DontDestroyOnLoad(MoneyUI);
        DontDestroyOnLoad(StockMarket);
        //DontDestroyOnLoad(GameObject.Find("EventSystem"));

        //periodically check if Player is Dead.If he is dead, display the GameOver Canvas.
        InvokeRepeating("GameOver",10f,3f);
        
    }

    private void Awake()
    {
        Debug.Log("Game Manager is awake");

        //making sure only one instance of the Game Manager exists.
        if(_instance!=null && _instance!=this)
        {
            Debug.Log("Game Manager already exists, deleting it now");
            Destroy(this.gameObject);
            return;
        }

        //this only executes if only one GameManager exists.
        _instance=this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Resume/Pause the game if the Escape key is held down.
        {
            if(isGamePaused && isGameOver==false)
            {
                 isGamePaused=false;
                PauseMenu.GetComponent<PauseMenuScript>().ResumeGame();
            }

            else{
                isGamePaused=true;
                PauseMenu.GetComponent<PauseMenuScript>().PauseGame();
            }
        }

        if(Input.GetKeyDown(KeyCode.P) &&isGameOver==false) //Activate or Deactivate the Stock Market canvas if the 'P' Key is held down.
        {
            if(isStockMarketOpen)
            {
                StockMarket.SetActive(false);
                isStockMarketOpen=false;
            }
            else
            {
                StockMarket.SetActive(true);
                isStockMarketOpen=true;

            }
        }
    }

    public void GameOver()
    {
        //if the player is dead, and the game is still running, stop the game and
        if(Player.GetComponent<PlayerScript>().isPlayerDead() && isGameOver==false)
        {
           Debug.Log("The Player is Dead");
        
           //Instantiate GameOver Canvas.
           GameOverScreen=(GameObject) GameObject.Instantiate(Resources.Load("GameOverCanvasUpdated"));
           
           //set isGameOver to be false, so that the canvas is not instantiated again by the GameManager.
           isGameOver=true;
           //show the canvas on screen.
           GameOverScreen.SetActive(true);
        }
    }
}
