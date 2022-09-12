using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Money;
    void Start()
    {
        //Debug.Log("Calling the Money Script");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void payWage(int value) // used by the location triggers to pay money per second to the player if they are in the office.
    {
      //Debug.Log("Paying Wage:"+ value);
      int newValue=(int.Parse(Money.GetComponent<Text>().text))+value;
      Debug.Log("New value is: "+newValue);
     Money.GetComponent<Text>().text=newValue.ToString();
    }

    public void setValue(int value) //used to set the value of the money canvas directly 
    {
        //Debug.Log("Setting the value of the MoneyBar");
        Money.GetComponent<Text>().text=value.ToString();

    }

    //utility method that other scripts can use to check how much money the player currently has.
    public int getValue()
    {
        int playerMoney= int.Parse(Money.GetComponent<Text>().text);
        //Debug.Log("The Player has: "+ playerMoney);

        return playerMoney;
    }
}
