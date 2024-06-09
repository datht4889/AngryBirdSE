using System;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    // public InputField gold;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI goldText;

    public Canvas logInPage;
    public Canvas signUpPage;
    public Canvas forgortPasswordPage;

    private string UserID;
    private DatabaseReference dbReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized and connected to the database.");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });

        TextMeshProUGUI usernamePlaceholder = username.placeholder as TextMeshProUGUI;
        usernamePlaceholder.text = "username";
        TextMeshProUGUI passwordPlaceholder = password.placeholder as TextMeshProUGUI;
        passwordPlaceholder.text = "password";
    }

    public void OpenSignUp(){

    }

    public void OpenForgortPassword(){

    }

    public void CreateUser()
    {
        string enteredUsername = username.text;

        // Check if the username already exists
        dbReference.Child("users").OrderByChild("username").EqualTo(enteredUsername).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Debug.LogError("Username already exists. Please choose another username.");
                }
                else
                {
                    // Username does not exist, proceed with user creation
                    UserID = Guid.NewGuid().ToString();
                    User newUser = new User(enteredUsername, password.text, 0);
                    string json = JsonUtility.ToJson(newUser);

                    dbReference.Child("users").Child(UserID).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            Debug.Log("Data written successfully!");
                        }
                        else
                        {
                            Debug.LogError("Error writing data: " + task.Exception);
                        }
                    });
                }
            }
            else
            {
                Debug.LogError("Error checking username: " + task.Exception);
            }
        });
    }

    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = dbReference.Child("users").Child(UserID).Child("username").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null && userNameData.Result.Exists)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallback.Invoke(snapshot.Value.ToString());
        }
        else
        {
            Debug.LogError("Error retrieving username data");
        }
    }

    public IEnumerator GetGold(Action<int> onCallback)
    {
        var userGoldData = dbReference.Child("users").Child(UserID).Child("gold").GetValueAsync();

        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if (userGoldData != null && userGoldData.Result.Exists)
        {
            DataSnapshot snapshot = userGoldData.Result;
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError("Error retrieving gold data");
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) =>
        {
            nameText.text = "Username: " + name;
        }));
        StartCoroutine(GetGold((int gold) =>
        {
            goldText.text = "Gold: " + gold.ToString();
        }));
    }

    public void UpdateName()
    {
        string newName = nameText.text.Replace("Username: ", "");
        dbReference.Child("users").Child(UserID).Child("username").SetValueAsync(newName).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Username updated successfully!");
            }
            else
            {
                Debug.LogError("Error updating username: " + task.Exception);
            }
        });
    }

    public void UpdateGold()
    {
        string goldStr = goldText.text.Replace("Gold: ", "");
        if (int.TryParse(goldStr, out int goldValue))
        {
            dbReference.Child("users").Child(UserID).Child("gold").SetValueAsync(goldValue).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Gold updated successfully!");
                }
                else
                {
                    Debug.LogError("Error updating gold: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("Invalid gold value");
        }
    }
}