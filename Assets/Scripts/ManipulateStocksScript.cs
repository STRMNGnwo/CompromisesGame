using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulateStocksScript : MonoBehaviour
{
    public GameObject BuyPrice;
    public GameObject Player;

    public GameObject MoneyCanvas;
    PlayerScript playerScript;
    int playerMoney;
    int currentPrice;
   
    // Start is called before the first frame update
    void Start()
    {
        currentPrice=int.Parse(BuyPrice.GetComponent<Text>().text);
        MoneyCanvas=GameObject.FindGameObjectWithTag("MONEYCANVAS");
        Player=GameObject.FindGameObjectWithTag("PLAYER");
        playerScript=Player.GetComponent<PlayerScript>();

        //setting the money the player has to show them how much is available for them to invest.
        GameObject.Find("AvailableFunds").GetComponent<Text>().text=MoneyCanvas.GetComponent<MoneyScript>().getValue().ToString();
        InvokeRepeating("ManipulateStock",1f,10f);
        InvokeRepeating("getAvailableFunds",0f,1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ManipulateStock()
    {
        int newValue;
        Debug.Log("Manipulating the Stock Market");
        //value that the stock should change by
        int valueChange= Random.Range(300,500);
        bool increase=true;
        
        currentPrice= int.Parse(BuyPrice.GetComponent<Text>().text);

        //random chance for the value of the stock to go up or go down.
        if(Random.Range(0,9)%2==0)
        {
          increase=false;
        }
        
        if(increase)
        {
            newValue=currentPrice+valueChange;
            BuyPrice.GetComponent<Text>().text= (newValue).ToString();
        }
           
        //decrease the stock value
        else
        {   
            //making sure that the stock does not go into negatives.
            if(currentPrice-valueChange<=0)
            {
               newValue=100;
            }
            else{
                newValue=currentPrice-valueChange;
            }
            BuyPrice.GetComponent<Text>().text= (newValue).ToString();

        }
         
         //if the change in value of stock is positive(stock has increased in value), display change in green.
        if(newValue-currentPrice>=0)
        {
            this.gameObject.transform.Find("ChangeValue").GetComponent<Text>().color=Color.green;
            this.gameObject.transform.Find("ChangeValue").GetComponent<Text>().text=(newValue-currentPrice).ToString();
        }

        //if the change in the value of stock is negative(the stock has decreased in value), display the change value in red.
        else{
            this.gameObject.transform.Find("ChangeValue").GetComponent<Text>().color=Color.red;
            this.gameObject.transform.Find("ChangeValue").GetComponent<Text>().text=(newValue-currentPrice).ToString();
        }
          
    }

   public void BuyStock()
    {
      currentPrice= int.Parse(BuyPrice.GetComponent<Text>().text); //price of the stock at this moment, is the price that the player pays to buy a single unit of stock.
      Debug.Log("Stock bought at: "+currentPrice);
      
      //Access the Money UI Canvas text. Convert it to int. If it is less than Buy Price, the user cannot buy stock.
      //If the value is higher, subtract the cost of the stock from the Money UI value and update the Money UI.
      //Once the stock is bought, add the stock to the user's stock hashtable.
       playerMoney=MoneyCanvas.GetComponent<MoneyScript>().getValue();

      Debug.Log("Amount of money the player has: "+playerMoney);
      Debug.Log("Trying to buy: "+ (this.gameObject.name));
      
      if(playerMoney>=currentPrice)
      {
          int stockCount=0;
          playerMoney-=currentPrice; //deducting the cost of the stock from the player's money.

          MoneyCanvas.GetComponent<MoneyScript>().setValue(playerMoney);
          
          //setting available funds 
          GameObject.Find("AvailableFunds").GetComponent<Text>().text=playerMoney.ToString();


          //add stock to the stocks hashtable of the player.

          if(playerScript.stocks.ContainsKey(this.gameObject.name)) //if the player already owns some of this stock, increment the stock count by 1.
          {
              Debug.Log("Player already owns this stock, adding more now");
              stockCount=(int)playerScript.stocks[this.gameObject.name];
              Debug.Log("Stock already owned by the player: "+stockCount);

              playerScript.stocks[this.gameObject.name]=(stockCount+1);
              this.gameObject.transform.Find("StocksOwned").GetComponent<Text>().text=(stockCount+1).ToString();
          }
          else{ //if the player does not own any of this stock before, add a new entry in the hashtable for this stock.
              Debug.Log("Player is buying this stock for the first time");
              playerScript.stocks.Add(this.gameObject.name,1);
              this.gameObject.transform.Find("StocksOwned").GetComponent<Text>().text=(stockCount+1).ToString();
          }

      }

      //don't do anything if the player does not have sufficient funds.
      else{
          Debug.Log("The Player does not have enough money to buy the stock yet");
        }

    }


    //method called when the Sell button for any stock is called.
    public void SellStock()
    {
        //get currentPrice of the stock
        currentPrice= int.Parse(BuyPrice.GetComponent<Text>().text);
         
        //if the player owns the stock, sell it, if they don't own any stock, don't do anything.
        if((int)playerScript.stocks[this.gameObject.name]<=0)
        {
            Debug.Log("Player does not have any stocks, cannot sell");
        }

        else{
            //reducing the count of stocks owned by the player after it is sold.
            playerScript.stocks[this.gameObject.name]=((int)playerScript.stocks[this.gameObject.name]-1);
            
            int stockCount=(int)playerScript.stocks[this.gameObject.name];
            
            //adding the profit made, if any to the player's money.
            playerMoney=MoneyCanvas.GetComponent<MoneyScript>().getValue();
            playerMoney+=currentPrice;

            MoneyCanvas.GetComponent<MoneyScript>().setValue(playerMoney);
            
            GameObject.Find("AvailableFunds").GetComponent<Text>().text=playerMoney.ToString();
            this.gameObject.transform.Find("StocksOwned").GetComponent<Text>().text=(stockCount).ToString();
        }

    }

    //utility function that is used to display the funds inside the device itself.
    public void getAvailableFunds()
    {
        GameObject AvailableFunds=GameObject.Find("AvailableFunds");
        if(AvailableFunds !=null)
        {
            AvailableFunds.GetComponent<Text>().text=MoneyCanvas.GetComponent<MoneyScript>().getValue().ToString();
        }   
    }
}

