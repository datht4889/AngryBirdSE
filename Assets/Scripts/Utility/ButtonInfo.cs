using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text priceText;
    public Text purchaseText;
    public ShopManager ShopManagerInstance;

    void Awake()
    {
        if (ShopManagerInstance != null)
        {
            priceText.text = ShopManagerInstance.shopItems[ItemID-1].price.ToString();
            
            if (ShopManagerInstance.shopItems[ItemID - 1].purchased)
            {
                purchaseText.text = "Purchased!";
            }
            else
            {
                purchaseText.text = "Unpurchased!";
            }
            
            
        }
           
    }    
    void Update()
    {   if (ShopManagerInstance != null)
    {
            if (ShopManagerInstance.shopItems[ItemID - 1].purchased)
            {
                purchaseText.text = "Purchased!";
            }
         
        }
        
}

}
