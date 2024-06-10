using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text priceText;
    public Text quantityText;
    public ShopManager ShopManagerInstance;


    void Update()
    {
        priceText.text = ShopManagerInstance.shopItems[2, ItemID].ToString();
        quantityText.text = ShopManagerInstance.shopItems[3, ItemID].ToString();
    }
}
