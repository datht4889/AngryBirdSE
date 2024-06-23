using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ShopManager;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text priceText;
    public Text purchaseText;
    public ShopManager ShopManagerInstance;
    private string ammoType;

    void Awake()
    {

        if (ItemID == 1)
        {
            ammoType = "biggerAmmo";
        }
        else
            ammoType = "explosionAmmo";

        StartCoroutine(DataManager.dataManager.GetAmmo(ammoType, (bool purchase1) =>
        {
            if (purchase1)
            {
                purchaseText.text = "Purchased!";
            }
            else
            {
                purchaseText.text = "Unpurchased!";
            }
        }));




    }    
    void Update()
    {   
            if (ShopManagerInstance.shopItems[ItemID - 1].purchased)
            {
                purchaseText.text = "Purchased!";
            }
        priceText.text = ShopManagerInstance.shopItems[ItemID - 1].price.ToString();

    }

}
