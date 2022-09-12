using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenScript : MonoBehaviour
{

     public string goToScene;
    // Start is called before the first frame update
    void Start()
    {
        // might need to get the current score(money) and display it
        Debug.Log("Instantiating the GameOverScreen");
        this.gameObject.transform.Find("MoneyScore").GetComponent<Text>().text=GameObject.FindGameObjectWithTag("MONEYCANVAS").GetComponent<MoneyScript>().getValue().ToString();
    }

     public void GameOver() //only used by the button on the GameOver Canvas
    {
        Debug.Log("Game Over");

        this.gameObject.SetActive(false);

        SceneManager.LoadScene(goToScene);

        //would need to destroy all the gameObjects in the Don't destroy on Load.

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
