using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4,4];

    public float money;
    public Text moneyTxt;
    void Start()
    {
        moneyTxt.text = money.ToString();

        //Id
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;

        //Price
        shopItems[2, 1] = 500;
        shopItems[2, 2] = 500;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Buy").GetComponent<EventSystem>().currentSelectedGameObject;

        if ( money >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            money -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            moneyTxt.text = money.ToString();
            
        }
    }

}

