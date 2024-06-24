using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public int money;
    public Text moneyTxt;
    public static ShopManager instances;
    private void Awake() { 
        if (instances == null)
        {
            instances = this;
        }

        StartCoroutine(DataManager.dataManager.GetAmmo("biggerAmmo", (bool purchase1) =>
        {
            shopItems[0] = new ShopItem { id = 1, name = "biggerAmmo", price = 2000, purchased = purchase1 };
            
            print(shopItems[0].price);
        }));

        StartCoroutine(DataManager.dataManager.GetAmmo("explosionAmmo", (bool purchase2) =>
        {
            shopItems[1] = new ShopItem { id = 1, name = "explosionAmmo", price = 3000, purchased = purchase2 };
        }));
        StartCoroutine(DataManager.dataManager.GetGold((int gold) =>
        {
            money = gold;
            print("tien" + money.ToString());
            moneyTxt.text = money.ToString();
        }));
        
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Buy").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemId = ButtonRef.GetComponent<ButtonInfo>().ItemID;
        ShopItem selectedItem = shopItems[itemId - 1]; // Assuming itemId is 1-based and matches array index + 1

        if (!selectedItem.purchased && money >= selectedItem.price)
        {
            print(money);
            print(selectedItem.price);
            print(selectedItem.purchased.ToString() + " " + selectedItem.name);
            money -= selectedItem.price;
            shopItems[itemId - 1].purchased = true;
            DataManager.dataManager.UpdateAmmo(shopItems[itemId - 1].name);
            moneyTxt.text = money.ToString();
            DataManager.dataManager.UpdateGold(-selectedItem.price);
            print(selectedItem.purchased.ToString() + " " + selectedItem.name);
        }
    }

    public static void LoadStart()
    {
        SceneManager.LoadScene("Assets/Scenes/StartMenu/StartScene.unity",LoadSceneMode.Single);
    }
}

