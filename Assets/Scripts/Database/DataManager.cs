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
    public TMP_InputField logInUsername;
    public TMP_InputField logInPassword;
    public TMP_InputField signUpUsername;
    public TMP_InputField signUpPassword;
    public TMP_InputField signUpConfirmPassword;
    // public InputField gold;

    public GameObject logInPage;
    public GameObject signUpPage;
    // public GameObject forgortPasswordPage;
    public TextMeshProUGUI logInFailed;
    public TextMeshProUGUI signUpFailed;

    private string UserID;
    private DatabaseReference dbReference;

    void Start()
    {
        OpenLogIn();
        logInPassword.contentType = TMP_InputField.ContentType.Password;
        signUpPassword.contentType = TMP_InputField.ContentType.Password;
        signUpConfirmPassword.contentType = TMP_InputField.ContentType.Password;
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
    }


    public void OpenLogIn(){
        logInPage.SetActive(true);
        signUpPage.SetActive(false);
        // forgortPasswordPage.SetActive(false);
        TextMeshProUGUI logInUsernamePlaceholder = logInUsername.placeholder as TextMeshProUGUI;
        logInUsernamePlaceholder.text = "username";
        TextMeshProUGUI logInPasswordPlaceholder = logInPassword.placeholder as TextMeshProUGUI;
        logInPasswordPlaceholder.text = "password";
    }
    public void OpenSignUp(){
        logInPage.SetActive(false);
        signUpPage.SetActive(true);
        // forgortPasswordPage.SetActive(false);
        TextMeshProUGUI signUpUsernamePlaceholder = signUpUsername.placeholder as TextMeshProUGUI;
        signUpUsernamePlaceholder.text = "username";
        TextMeshProUGUI signUpPasswordPlaceholder = signUpPassword.placeholder as TextMeshProUGUI;
        signUpPasswordPlaceholder.text = "password";
    }

    // public void OpenForgortPassword(){
    //     logInPage.SetActive(false);
    //     signUpPage.SetActive(false);
    //     forgortPasswordPage.SetActive(true);
    // }

    public void LogIn(){
        string enteredUsername = logInUsername.text;
        string enteredPassword = logInPassword.text;

        if(string.IsNullOrEmpty(enteredUsername)||string.IsNullOrEmpty(enteredPassword)){
            // logInFailed.SetActive(true);
            logInFailed.text = "Username and Password cannot be empty";
        }else{
            dbReference.Child("users").OrderByChild("username").EqualTo(enteredUsername).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.Exists)
                    {
                        foreach (var userSnapshot in snapshot.Children)
                        {
                            IDictionary userDict = (IDictionary)userSnapshot.Value;
                            string storedPassword = userDict["password"].ToString();

                            if (storedPassword == enteredPassword)
                            {
                                UserID = userSnapshot.Key;
                                logInFailed.text = "Login successful!";
                                Button.instances.Exit2Menu();
                                // Optionally, load user data or transition to another scene
                            }
                            else
                            {
                                logInFailed.text = "Incorrect password. Please try again.";
                            }
                        }
                    }
                    else
                    {
                        logInFailed.text = "Username does not exist. Please register.";
                    }
                }
                else
                {
                    logInFailed.text = "Error checking username: " + task.Exception;
                }
            });
        }
    }

    public void SignUp()
    {
        string enteredUsername = signUpUsername.text;
        string enteredPassword = signUpPassword.text;
        string confirmedPassword = signUpConfirmPassword.text;

        // Check if the username already exists
        if(string.IsNullOrEmpty(enteredUsername)||string.IsNullOrEmpty(enteredPassword)||string.IsNullOrEmpty(confirmedPassword)){
            // logInFailed.SetActive(true);
            signUpFailed.text = "Username and Password cannot be empty";
        } else{
            dbReference.Child("users").OrderByChild("username").EqualTo(enteredUsername).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("task done");

                    if (snapshot.Exists)
                    {
                        // Username already exists, display error message
                        signUpFailed.text = "Username already exists. Please choose another username.";
                        Debug.Log("exist");
                    }
                    else
                    {
                        // Check if the password matches the confirmed password
                        // if (enteredPassword.Equals(confirmedPassword))
                        // {
                        //     // Passwords match, proceed with user creation
                        Debug.Log("else");
                            UserID = Guid.NewGuid().ToString();
                            User newUser = new User(enteredUsername, enteredPassword, 0);
                            string json = JsonUtility.ToJson(newUser);

                            dbReference.Child("users").Child(UserID).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
                            {
                                if (task.IsCompleted)
                                {
                                    signUpFailed.text = "Sign up successfully!";
                                }
                                else
                                {
                                    signUpFailed.text = "Error sign up: " + task.Exception;
                                }
                            });
                        // }
                        // else
                        // {
                        //     // Passwords don't match, display error message
                        //     signUpFailed.text = "Confirm password doesn't match.";
                        // }
                    }
                }
                else
                {
                    signUpFailed.text = "Error checking username: " + task.Exception;
                    Debug.Log("lmao");
                }
            });
        }
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

    // public void GetUserInfo()
    // {
    //     StartCoroutine(GetName((string name) =>
    //     {
    //         nameText.text = "Username: " + name;
    //     }));
    //     StartCoroutine(GetGold((int gold) =>
    //     {
    //         goldText.text = "Gold: " + gold.ToString();
    //     }));
    // }

    // public void UpdateName()
    // {
    //     string newName = nameText.text.Replace("Username: ", "");
    //     dbReference.Child("users").Child(UserID).Child("username").SetValueAsync(newName).ContinueWithOnMainThread(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             Debug.Log("Username updated successfully!");
    //         }
    //         else
    //         {
    //             Debug.LogError("Error updating username: " + task.Exception);
    //         }
    //     });
    // }

    // public void UpdateGold()
    // {
    //     string goldStr = goldText.text.Replace("Gold: ", "");
    //     if (int.TryParse(goldStr, out int goldValue))
    //     {
    //         dbReference.Child("users").Child(UserID).Child("gold").SetValueAsync(goldValue).ContinueWithOnMainThread(task =>
    //         {
    //             if (task.IsCompleted)
    //             {
    //                 Debug.Log("Gold updated successfully!");
    //             }
    //             else
    //             {
    //                 Debug.LogError("Error updating gold: " + task.Exception);
    //             }
    //         });
    //     }
    //     else
    //     {
    //         Debug.LogError("Invalid gold value");
    //     }
    // }


}