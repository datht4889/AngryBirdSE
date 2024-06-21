using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public struct ShopItem
    {
        public int id;
        public int price;
        public bool purchased;
        public string name;
    }

    public ShopItem[] shopItems = new ShopItem[2];

    public float money;
    public Text moneyTxt;

    void Start()
    {
        moneyTxt.text = money.ToString();
        StartCoroutine(DataManager.dataManager.GetAmmo("biggerAmmo", (bool purchase1) =>
        {
            shopItems[0] = new ShopItem { id = 1, name = "biggerAmmo", price = 500, purchased = purchase1 };
        }));

        StartCoroutine(DataManager.dataManager.GetAmmo("explosionAmmo", (bool purchase2) =>
        {
            shopItems[1] = new ShopItem { id = 1, name = "explosionAmmo", price = 500, purchased = purchase2 };
        }));
        StartCoroutine(DataManager.dataManager.GetGold((int gold) =>
        {
            money = gold;
        }));
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Buy").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemId = ButtonRef.GetComponent<ButtonInfo>().ItemID;
        ShopItem selectedItem = shopItems[itemId - 1]; // Assuming itemId is 1-based and matches array index + 1

        if (!selectedItem.purchased && money >= selectedItem.price)
        {
            money -= selectedItem.price;
            shopItems[itemId - 1].purchased = true;
            DataManager.dataManager.UpdateAmmo(shopItems[itemId - 1].name, shopItems[itemId - 1].purchased);
            moneyTxt.text = money.ToString();
        }
    }
}
