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
    {   if (ShopManagerInstance != null)
        {
            priceText.text = ShopManagerInstance.shopItems[2, ItemID].ToString();
            if (ItemID != 0)
            {
                quantityText.text = ShopManagerInstance.shopItems[3, ItemID].ToString();
            }
        }
        
    }
}
