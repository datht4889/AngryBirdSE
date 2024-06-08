using System;
using System.Collections;
using Firebase;
using Firebase.Database;
using Google.MiniJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DataManager: MonoBehaviour {

    public InputField username;
    public InputField password;
    // public InputField gold;
    private string UserID;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI goldText;

    DatabaseReference dbReference;
    void Start() {
        UserID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser(){
        User newUser = new User(username.text, password.text, 0);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(UserID).SetRawJsonValueAsync(json);
    }

    public IEnumerator GetName(Action<string> onCallback){
        var userNameData = dbReference.Child("users").Child(UserID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null){
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    // public IEnumerator GetGold(Action<int> onCallback){
    //     var userGoldData = dbReference.Child("users").Child(UserID).Child("gold").GetValueAsync();

    //     yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

    //     if (userGoldData != null){
    //         DataSnapshot snapshot = userGoldData.Result;

    //         onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
    //     }
    // }

    // public void GetUserInfo(){
    //     StartCoroutine(GetName((string name) => {
    //         nameText.text = "Name: "+name;
    //     }));
    //     StartCoroutine(GetGold((int gold) => {
    //         goldText.text = "Gold: "+gold.ToString();
    //     }));
    // }

    // public void UpdateName(){

    // }
}